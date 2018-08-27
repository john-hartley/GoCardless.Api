using Newtonsoft.Json;

namespace GoCardlessApi.Refunds
{
    public class UpdateRefundResponse
    {
        [JsonProperty("payment_refunds")]
        public Refund Refund { get; set; }
    }
}