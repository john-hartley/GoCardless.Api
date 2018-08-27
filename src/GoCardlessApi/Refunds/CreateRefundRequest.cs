using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.Refunds
{
    public class CreateRefundRequest
    {
        public CreateRefundRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("links")]
        public CreateRefundLinks Links { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("total_amount_confirmation")]
        public int TotalAmountConfirmation { get; set; }
    }
}