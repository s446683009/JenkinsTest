using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCR.EventBus
{
    public interface IEventHandler<T> where T:IEvent
    {
        Task Handle(T t);
    }
}
