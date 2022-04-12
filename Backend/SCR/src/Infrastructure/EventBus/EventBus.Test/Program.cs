using System;
using EventRabbitMQBus;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
namespace EventBus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //IServiceCollection services = new ServiceCollection();
            //services.AddSingleton<IConnectionFactory>(s => {
            //    var factory= new ConnectionFactory();
            //    factory.UserName = "Admin";
            //    factory.Password = "";
            //    return factory;
            //});

            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            //services.AddSingleton<IRabbitMQConnection>(s => {
            //    return new DefaultRabbitMQConnection();
            
            //});


        }
    }
}
