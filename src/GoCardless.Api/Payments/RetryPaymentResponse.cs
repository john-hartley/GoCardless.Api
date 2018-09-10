using Newtonsoft.Json;

namespace GoCardless.Api.Payments
{
    public class RetryPaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}