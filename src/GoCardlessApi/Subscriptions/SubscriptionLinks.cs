using Newtonsoft.Json;

namespace GoCardlessApi.Subscriptions
{
    public class SubscriptionLinks
    {
        [JsonProperty("mandate")]
        public string Mandate { get; set; }
    }
}