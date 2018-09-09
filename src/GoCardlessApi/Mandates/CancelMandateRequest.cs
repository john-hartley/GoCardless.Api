using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardless.Api.Mandates
{
    public class CancelMandateRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}