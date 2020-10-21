using System;
using System.Collections.Generic;

namespace GoCardlessApi.Subscriptions
{
    public class Subscription
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public int? AppFee { get; set; }
        public int? Count { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Currency { get; set; }
        public int? DayOfMonth { get; set; }
        public DateTime? EndDate { get; set; }
        public int Interval { get; set; }

        /// <summary>
        /// See <see cref="Subscriptions.IntervalUnit"/> for possible values.
        /// </summary>
        public string IntervalUnit { get; set; }

        public SubscriptionLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// See <see cref="Subscriptions.Month"/> for possible values.
        /// </summary>
        public string Month { get; set; }

        public string Name { get; set; }
        public string PaymentReference { get; set; }
        public DateTime StartDate { get; set; }

        /// <summary>
        /// See <see cref="Subscriptions.SubscriptionStatus"/> for possible values.
        /// </summary>
        public string Status { get; set; }

        public IEnumerable<UpcomingPayment> UpcomingPayments { get; set; }
    }
}