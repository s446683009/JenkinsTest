using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Interfaces
{
    public interface IEventHandler<T> where T:BaseEvent
    {

        Task Handle(T @event);
    }
}
