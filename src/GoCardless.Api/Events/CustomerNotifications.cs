﻿using System;

namespace GoCardlessApi.Events
{
    public class CustomerNotifications
    {
        public string Id { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public bool Mandatory { get; set; }
        public string Type { get; set; }
    }
}