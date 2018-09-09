using Newtonsoft.Json;

namespace GoCardless.Api.Creditors
{
    public class CreditorResponse
    {
        [JsonProperty("creditors")]
        public Creditor Creditor { get; set; }
    }
}