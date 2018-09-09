using Newtonsoft.Json;

namespace GoCardless.Api.Payments
{
    public class CancelPaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}