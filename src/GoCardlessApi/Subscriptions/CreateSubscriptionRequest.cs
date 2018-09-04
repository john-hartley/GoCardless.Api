using System;
using System.Collections.Generic;

namespace GoCardlessApi.Subscriptions
{
    public class CreateSubscriptionRequest
    {
        public int Amount { get; set; }
        public int? AppFee { get; set; }
        public int Count { get; set; }
        public string Currency { get; set; }
        public int? DayOfMonth { get; set; }

        [Obsolete("Deprecated: This field will be removed in a future API version. Use the Count property to specify a number of payments instead.")]
        public string EndDate { get; set; }

        public int Interval { get; set; }
        public string IntervalUnit { get; set; }
        public SubscriptionLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Month { get; set; }
        public string Name { get; set; }
        public string PaymentReference { get; set; }
        public string StartDate { get; set; }
    }
}