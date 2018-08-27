using Newtonsoft.Json;
using System;

namespace GoCardlessApi.Payouts
{
    public class Payout
    {
        public string Id { get; set; }

        public int Amount { get; set; }

        [JsonProperty("arrival_date")]
        public DateTime ArrivalDate { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        public string Currency { get; set; }

        [JsonProperty("deducted_fees")]
        public int DeductedFees { get; set; }

        public PayoutLinks Links { get; set; }

        [JsonProperty("payout_type")]
        public string PayoutType { get; set; }

        public string Reference { get; set; }

        public string Status { get; set; }
    }
}