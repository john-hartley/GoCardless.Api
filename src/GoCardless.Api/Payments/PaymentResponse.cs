using Newtonsoft.Json;

namespace GoCardless.Api.Payments
{
    public class PaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}