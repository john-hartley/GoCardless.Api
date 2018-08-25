using Newtonsoft.Json;

namespace GoCardlessApi.Subscriptions
{
    public class CancelSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}