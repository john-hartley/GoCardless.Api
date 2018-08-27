using Newtonsoft.Json;

namespace GoCardlessApi.Refunds
{
    public class RefundResponse
    {
        [JsonProperty("refunds")]
        public Refund Refund { get; set; }
    }
}