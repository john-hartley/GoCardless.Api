using Newtonsoft.Json;

namespace GoCardless.Api.Refunds
{
    public class CreateRefundResponse
    {
        [JsonProperty("refunds")]
        public Refund Refund { get; set; }
    }
}