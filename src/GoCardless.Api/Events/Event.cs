using System;
using System.Collections.Generic;

namespace GoCardless.Api.Events
{
    public class Event
    {
        public string Id { get; set; }

        public string Action { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public CustomerNotifications CustomerNotifications { get; set; }
        public Details Details { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string ResourceType { get; set; }
        public EventLinks Links { get; set; }
    }
}