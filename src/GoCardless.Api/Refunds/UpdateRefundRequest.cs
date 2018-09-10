using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardless.Api.Refunds
{
    public class UpdateRefundRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}