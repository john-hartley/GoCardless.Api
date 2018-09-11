using Newtonsoft.Json;

namespace GoCardless.Api.Subscriptions
{
    public class UpdateSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}