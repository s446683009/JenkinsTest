
using EventBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Aggregates.Events
{
    record PasswordChangeEvent:BaseEvent
    {
        public string UserId { get; set; }
        public string Account { get; set; }
        public string ChangeTime { get; set; }
        public DateTime EventTime { get; set; }
    }
}
