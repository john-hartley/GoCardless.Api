using System;
using System.Collections.Generic;

namespace GoCardlessApi.Subscriptions
{
    public class Subscription
    {
        public string Id { get; set; }

        public int Amount { get; set; }

        public int? AppFee { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Currency { get; set; }

        public int? DayOfMonth { get; set; }

        public DateTime? EndDate { get; set; }

        public int Interval { get; set; }

        public string IntervalUnit { get; set; }

        public SubscriptionLinks Links { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public string Month { get; set; }

        public string Name { get; set; }

        public string PaymentReference { get; set; }

        public DateTime StartDate { get; set; }

        public string Status { get; set; }

        public IEnumerable<UpcomingPayment> UpcomingPayments { get; set; }
    }
}