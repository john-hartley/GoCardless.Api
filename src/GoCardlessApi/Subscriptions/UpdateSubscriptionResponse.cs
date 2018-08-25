using Newtonsoft.Json;

namespace GoCardlessApi.Subscriptions
{
    public class UpdateSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}