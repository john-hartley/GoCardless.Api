using Newtonsoft.Json;

namespace GoCardless.Api.Subscriptions
{
    public class CancelSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}