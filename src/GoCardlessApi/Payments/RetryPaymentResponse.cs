using Newtonsoft.Json;

namespace GoCardlessApi.Payments
{
    public class RetryPaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}