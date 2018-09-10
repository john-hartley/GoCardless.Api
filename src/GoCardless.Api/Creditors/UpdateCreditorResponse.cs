using Newtonsoft.Json;

namespace GoCardless.Api.Creditors
{
    public class UpdateCreditorResponse
    {
        [JsonProperty("creditors")]
        public Creditor Creditor { get; set; }
    }
}