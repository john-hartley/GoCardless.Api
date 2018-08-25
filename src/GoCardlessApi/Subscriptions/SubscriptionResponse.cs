using Newtonsoft.Json;

namespace GoCardlessApi.Subscriptions
{
    public class SubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}