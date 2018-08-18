using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class CreateSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}