using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Refunds
{
    public class UpdateRefundRequest
    {
        public UpdateRefundRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }
    }
}