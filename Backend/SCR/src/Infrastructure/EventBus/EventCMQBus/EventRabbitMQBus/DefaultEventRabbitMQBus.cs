using EventBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Polly.Retry;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using Polly;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Collections.Concurrent;
using RabbitMQ.Client.Events;
namespace EventRabbitMQBus
{
    public class DefaultEventRabbitMQBus : IEventBus
    {
        IRabbitMQConnection _connection;
        ILogger<DefaultEventRabbitMQBus> _logger;
        private IModel _consumerChannel;
        private string _queueName;
        private IServiceProvider _serviceProvider;
        private const string ExchangeName = "SCR_EventBus_Exchange";
        private readonly ConcurrentDictionary<Type, List<Type>> _eventMapping = new ConcurrentDictionary<Type, List<Type>>();
        public DefaultEventRabbitMQBus(IServiceProvider serviceProvider, string queueName,IRabbitMQConnection connection, ILogger<DefaultEventRabbitMQBus> logger) {
            _connection = connection;
            _logger =logger;
            _queueName = queueName;
            _serviceProvider = serviceProvider;
        }

        private IModel CreateConsumerChannel() {

            var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: ExchangeName,
                                    type: "direct");

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;

        }
        private void DoInternalSubscription(string eventName)
        {
            
            if (!_eventMapping.Keys.Any(t => t.Name == eventName))
            {
                if (!_connection.IsConnect)
                {
                    _connection.Connect();
                }

                _consumerChannel.QueueBind(queue: _queueName,
                                    exchange: ExchangeName,
                                    routingKey: eventName);
            }
        }
        private void StartBasicConsume()
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
            }

            // Even on exception we take the message off the queue.
            // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
            // For more information see: https://www.rabbitmq.com/dlx.html
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);
            if (_eventMapping.Keys.Any(t=>t.Name==eventName))
            {
               
                var typeSubcs =_eventMapping.FirstOrDefault(t=>t.Key.Name==eventName);
                var integrationEvent = JsonSerializer.Deserialize(message, typeSubcs.Key, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                foreach (var handler in typeSubcs.Value)
                {

                    var handlerObj = _serviceProvider.GetService(handler);
                    var methodInfo = handlerObj.GetType().GetMethod(nameof(IEventHandler<BaseEvent>.Handle));
                    await Task.Yield();
                    await (Task)methodInfo.Invoke(handler, new object[] { integrationEvent });


                }

            }




        
            else
            {
                _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
            }
        }
        public void Publish(BaseEvent @event)
        {

            if (!_connection.IsConnect)
            {
                _connection.Connect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var eventName = @event.GetType().Name;

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

            using (var channel = _connection.CreateModel())
            {
                _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

                channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

                var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

                    channel.BasicPublish(
                        exchange: ExchangeName,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });
            }
        }

        public void RegisterHandler(IServiceCollection services, IEnumerable<Type> eventHandlerTypes)
        {
            foreach (var type in eventHandlerTypes)
            {
                var interfaceTypes = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>)).ToList();
                if (interfaceTypes.Count == 0) continue;


                foreach (var interfaceType in interfaceTypes)
                {
                    var messageType = interfaceType.GetGenericArguments().First();
                    var handleMethod =
                        type.GetMethods().FirstOrDefault(
                            m => m.Name == nameof(IEventHandler<BaseEvent>.Handle) &&
                               m.GetParameters().First().ParameterType == messageType &&
                               m.GetParameters().Length == 1
                            );
                    if (handleMethod == null)
                        continue;
                    List<Type> handlerTypes = _eventMapping[messageType];
                    if (!handlerTypes.Contains(messageType))
                    {
                        handlerTypes.Add(interfaceType);
                        //_eventAndHandlerMapping[type] = handlerTypes;
                    }

                }

                services.AddSingleton(type);
            }

        }

        public void Subscribe<T, TH>()
            where T : BaseEvent
            where TH : IEventHandler<BaseEvent>
        {
            var type = typeof(T);
            var eventType = typeof(TH);
            List<Type> handlerTypes = _eventMapping[type];
            if (!handlerTypes.Contains(eventType))
            {
                handlerTypes.Add(eventType);
                DoInternalSubscription(type.Name);
            }
     
        }

        public void Unsubscribe<T, TH>()
            where T : BaseEvent
            where TH : IEventHandler<BaseEvent>
        {
            var type = typeof(T);
            var eventType = typeof(TH);
            List<Type> handlerTypes = _eventMapping[type];
            if (handlerTypes.Contains(eventType))
            {
                handlerTypes.Remove(eventType);

            }
        }
    }
}
