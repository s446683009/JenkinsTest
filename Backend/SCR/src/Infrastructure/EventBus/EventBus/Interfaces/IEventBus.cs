using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Interfaces
{
    public interface IEventBus
    {

        void Publish(BaseEvent @event);
        void Subscribe<T, TH>() where T : BaseEvent
            where TH : IEventHandler<BaseEvent>;
        void Unsubscribe<T, TH>()
        where T : BaseEvent
        where TH : IEventHandler<BaseEvent>;
        void RegisterHandler(IServiceCollection services, IEnumerable<Type> eventHandlerTypes);

    }
}
