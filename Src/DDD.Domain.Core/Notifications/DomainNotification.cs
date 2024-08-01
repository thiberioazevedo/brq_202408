using System;
using DDD.Domain.Core.Events;

namespace DDD.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }
        public string Type { get; private set; }
        public string Message { get; private set; }
        public int Version { get; private set; }
        public long Id { get; private set; }

        public DomainNotification(string type, string message)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Type = type;
            Message = message;
        }

        public DomainNotification(string type, string message, long id)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Type = type;
            Message = message;
            this.Id = id;
        }
    }
}
