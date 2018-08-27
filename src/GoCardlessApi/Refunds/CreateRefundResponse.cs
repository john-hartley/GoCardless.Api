using Newtonsoft.Json;

namespace GoCardlessApi.Refunds
{
    public class CreateRefundResponse
    {
        [JsonProperty("refunds")]
        public Refund Refund { get; set; }
    }
}