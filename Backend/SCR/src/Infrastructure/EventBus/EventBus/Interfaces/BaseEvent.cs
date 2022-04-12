using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Interfaces
{
    public record BaseEvent
    {

        public BaseEvent()
        {
            CreatedTime = DateTime.Now;
            Id = Guid.NewGuid();

        }
        public BaseEvent(string name) : this()
        {
            Name = name;
        }
        public Guid Id { get; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; }
    }
}
