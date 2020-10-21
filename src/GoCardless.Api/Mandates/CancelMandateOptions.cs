using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Mandates
{
    public class CancelMandateOptions
    {
        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}