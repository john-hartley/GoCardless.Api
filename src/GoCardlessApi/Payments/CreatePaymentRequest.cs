using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.Payments
{
    public class CreatePaymentRequest
    {
        public CreatePaymentRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("app_fee")]
        public int? AppFee { get; set; }

        [JsonProperty("charge_date")]
        public string ChargeDate { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("links")]
        public CreatePaymentLinks Links { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}