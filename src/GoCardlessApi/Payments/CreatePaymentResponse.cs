using Newtonsoft.Json;

namespace GoCardless.Api.Payments
{
    public class CreatePaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}