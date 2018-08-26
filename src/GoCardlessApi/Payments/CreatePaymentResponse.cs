using Newtonsoft.Json;

namespace GoCardlessApi.Payments
{
    public class CreatePaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}