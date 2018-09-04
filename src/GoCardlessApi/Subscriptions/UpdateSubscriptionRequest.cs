using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Subscriptions
{
    public class UpdateSubscriptionRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public int Amount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? AppFee { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }
        public string PaymentReference { get; set; }
    }
}