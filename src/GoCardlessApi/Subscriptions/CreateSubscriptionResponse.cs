using Newtonsoft.Json;

namespace GoCardless.Api.Subscriptions
{
    public class CreateSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}