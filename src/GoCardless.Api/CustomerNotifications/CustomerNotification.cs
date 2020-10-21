using System;

namespace GoCardlessApi.CustomerNotifications
{
    public class CustomerNotification
    {
        public string Id { get; set; }

        /// <summary>
        /// See <see cref="CustomerNotifications.ActionTaken"/> for possible values.
        /// </summary>
        public string ActionTaken { get; set; }

        public DateTimeOffset ActionTakenAt { get; set; }
        public string ActionTakenBy { get; set; }
        public CustomerNotificationLinks Links { get; set; }
        public string Type { get; set; }
    }
}