using System;
using System.Collections.Generic;

namespace GoCardless.Api.Events
{
    public class Event
    {
        public string Id { get; set; }

        /// <summary>
        /// See <see cref="Events.Actions"/> for possible values.
        /// </summary>
        public string Action { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public CustomerNotifications CustomerNotifications { get; set; }
        public Details Details { get; set; }
        public EventLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// See <see cref="Events.ResourceType"/> for possible values.
        /// </summary>
        public string ResourceType { get; set; }
    }
}