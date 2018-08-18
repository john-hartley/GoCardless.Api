using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi
{
    public class UpdateSubscriptionRequest
    {
        public UpdateSubscriptionRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonIgnore]
        public string Id { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("app_fee")]
        public int AppFee { get; set; }
        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("payment_reference")]
        public string PaymentReference { get; set; }
    }
}