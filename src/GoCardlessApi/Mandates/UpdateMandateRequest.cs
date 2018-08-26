using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Mandates
{
    public class UpdateMandateRequest
    {
        public UpdateMandateRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }
    }
}