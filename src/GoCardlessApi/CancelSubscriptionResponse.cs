using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class CancelSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}