using Newtonsoft.Json;

namespace GoCardlessApi.Payments
{
    public class PaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}