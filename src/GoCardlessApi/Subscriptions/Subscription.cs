using GoCardlessApi.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.Subscriptions
{
    public class Subscription
    {
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        public int Amount { get; set; }

        public string Currency { get; set; }

        public string Status { get; set; }

        public string Name { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }

        public int Interval { get; set; }

        [JsonProperty("interval_unit")]
        public string IntervalUnit { get; set; }

        [JsonProperty("day_of_month")]
        public int? DayOfMonth { get; set; }

        public string Month { get; set; }

        [JsonProperty("payment_reference")]
        public string PaymentReference { get; set; }

        [JsonProperty("upcoming_payments")]
        public IEnumerable<UpcomingPayment> UpcomingPayments { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("app_fee")]
        public int? AppFee { get; set; }

        public Links Links { get; set; }

        public Meta Meta { get; set; }
    }
}
