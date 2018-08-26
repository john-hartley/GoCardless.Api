using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.Payments
{
    public class Payment
    {
        public string Id { get; set; }

        public int Amount { get; set; }

        [JsonProperty("amount_refunded")]
        public int AmountRefunded { get; set; }

        [JsonProperty("app_fee")]
        public int? AppFee { get; set; }

        [JsonProperty("charge_date")]
        public string ChargeDate { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public PaymentLinks Links { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public string Reference { get; set; }

        public string Status { get; set; }
    }
}