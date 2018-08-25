using Newtonsoft.Json;

namespace GoCardlessApi.Subscriptions
{
    public class CreateSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}