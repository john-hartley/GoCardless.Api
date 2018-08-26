using Newtonsoft.Json;

namespace GoCardlessApi.Payments
{
    public class CreatePaymentLinks
    {
        [JsonProperty("mandate")]
        public string Mandate { get; set; }
    }
}