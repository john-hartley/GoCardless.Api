using Newtonsoft.Json;

namespace GoCardless.Api.Refunds
{
    public class UpdateRefundResponse
    {
        [JsonProperty("payment_refunds")]
        public Refund Refund { get; set; }
    }
}