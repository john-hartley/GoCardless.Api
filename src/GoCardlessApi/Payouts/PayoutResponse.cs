using Newtonsoft.Json;

namespace GoCardlessApi.Payouts
{
    public class PayoutResponse
    {
        [JsonProperty("payouts")]
        public Payout Payout { get; set; }
    }
}