using Newtonsoft.Json;

namespace GoCardless.Api.Payouts
{
    public class PayoutResponse
    {
        [JsonProperty("payouts")]
        public Payout Payout { get; set; }
    }
}