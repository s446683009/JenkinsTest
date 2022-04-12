using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EventRabbitMQBus
{
    public interface IRabbitMQConnection:IDisposable
    {
        bool IsConnect { get; }
        bool Connect();
        IModel CreateModel();

    }
}
