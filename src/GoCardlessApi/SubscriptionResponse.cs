using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class SubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}