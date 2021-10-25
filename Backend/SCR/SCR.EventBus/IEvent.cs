using System;
using System.Collections.Generic;
using System.Text;

namespace SCR.EventBus
{
    public interface IEvent
    {
        public DateTime EventTime { get; set; }
    }
}
