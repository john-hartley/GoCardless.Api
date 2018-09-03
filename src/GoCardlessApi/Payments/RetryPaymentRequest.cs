using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Payments
{
    public class RetryPaymentRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}