using SCR.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCR.Domain.Aggregates.Events
{
    class PasswordChangeEvent:IEvent
    {
        public string UserId { get; set; }
        public string Account { get; set; }
        public string ChangeTime { get; set; }
        public DateTime EventTime { get; set; }
    }
}
