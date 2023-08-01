using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Identity.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            _domainEvents = new List<INotification>();
        }

        private List<INotification> _domainEvents { get; set; }
        public DateTime CreatedTime { get; protected set; }

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvents(INotification notification)
        {
            _domainEvents.Add(notification);
        }
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

    }
}
