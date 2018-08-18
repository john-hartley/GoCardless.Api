using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class UpdateSubscriptionResponse
    {
        [JsonProperty("subscriptions")]
        public Subscription Subscription { get; set; }
    }
}