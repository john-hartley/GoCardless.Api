using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.Payments
{
    public class RetryPaymentOptions
    {
        [JsonIgnore]
        public string Id { get; set; }

        public DateTime? ChargeDate { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}