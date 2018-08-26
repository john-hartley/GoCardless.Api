using Newtonsoft.Json;

namespace GoCardlessApi.Payments
{
    public class PaymentLinks
    {
        [JsonProperty("creditor")]
        public string Creditor { get; set; }

        [JsonProperty("mandate")]
        public string Mandate { get; set; }
    }
}