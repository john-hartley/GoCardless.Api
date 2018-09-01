using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Subscriptions
{
    public class CancelSubscriptionRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}