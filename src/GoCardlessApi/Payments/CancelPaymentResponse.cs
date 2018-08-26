using Newtonsoft.Json;

namespace GoCardlessApi.Payments
{
    public class CancelPaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}