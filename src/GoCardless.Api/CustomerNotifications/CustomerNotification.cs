using System;

namespace GoCardless.Api.CustomerNotifications
{
    public class CustomerNotification
    {
        public string Id { get; set; }
        public string ActionTaken { get; set; }
        public DateTimeOffset ActionTakenAt { get; set; }
        public string ActionTakenBy { get; set; }
        public string Type { get; set; }
        public CustomerNotificationLinks Links { get; set; }
    }
}