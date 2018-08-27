using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.Subscriptions
{
    public class CreateSubscriptionRequest
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("app_fee")]
        public int? AppFee { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("day_of_month")]
        public int? DayOfMonth { get; set; }

        [JsonProperty("end_date")]
        [Obsolete("Deprecated: This field will be removed in a future API version. Use the Count property to specify a number of payments instead.")]
        public string EndDate { get; set; }

        [JsonProperty("interval")]
        public int Interval { get; set; }

        [JsonProperty("interval_unit")]
        public string IntervalUnit { get; set; }

        [JsonProperty("links")]
        public SubscriptionLinks Links { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("month")]
        public string Month { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("payment_reference")]
        public string PaymentReference { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }
    }
}