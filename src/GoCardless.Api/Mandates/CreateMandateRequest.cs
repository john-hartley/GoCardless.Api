using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardless.Api.Mandates
{
    public class CreateMandateRequest
    {
        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public CreateMandateLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string Reference { get; set; }
        public string Scheme { get; set; }
    }
}