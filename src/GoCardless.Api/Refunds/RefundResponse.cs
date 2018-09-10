using Newtonsoft.Json;

namespace GoCardless.Api.Refunds
{
    public class RefundResponse
    {
        [JsonProperty("refunds")]
        public Refund Refund { get; set; }
    }
}