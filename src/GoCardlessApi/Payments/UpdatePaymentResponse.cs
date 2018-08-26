using Newtonsoft.Json;

namespace GoCardlessApi.Payments
{
    public class UpdatePaymentResponse
    {
        [JsonProperty("payments")]
        public Payment Payment { get; set; }
    }
}