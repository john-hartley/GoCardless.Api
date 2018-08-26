using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Mandates
{
    public class CreateMandateRequest
    {
        public CreateMandateRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonProperty("links")]
        public CreateMandateLinks Links { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("scheme")]
        public string Scheme { get; set; }
    }
}