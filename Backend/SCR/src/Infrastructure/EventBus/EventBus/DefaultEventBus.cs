using EventBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class DefaultEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        public DefaultEventBus(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        private readonly ConcurrentDictionary<Type, List<Type>> _eventMapping = new ConcurrentDictionary<Type, List<Type>>();
        public void Publish(BaseEvent @event)
        {
            HandleEvent(@event);
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
                //_eventAndHandlerMapping[type] = handlerTypes;
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
        public void HandleEvent(BaseEvent @event) {
            List<Type> handlers = _eventMapping[typeof(BaseEvent)];

            if (handlers != null && handlers.Count > 0)
            {
                foreach (var handler in handlers)
                {

                    var obj = _serviceProvider.GetService(handler);
                    var methodInfo = obj.GetType().GetMethod(nameof(IEventHandler<BaseEvent>.Handle));

                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(obj, new object[] { @event });
                    }
                }
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
    }
}
