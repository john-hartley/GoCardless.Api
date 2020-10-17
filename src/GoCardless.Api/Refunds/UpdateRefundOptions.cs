using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Refunds
{
    public class UpdateRefundOptions
    {
        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}