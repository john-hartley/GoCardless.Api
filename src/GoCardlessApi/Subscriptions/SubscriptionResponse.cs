using Newtonsoft.Json;

namespace GoCardless.Api.Subscriptions
{
    public class SubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}