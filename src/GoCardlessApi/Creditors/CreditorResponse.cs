using Newtonsoft.Json;

namespace GoCardlessApi.Creditors
{
    public class CreditorResponse
    {
        [JsonProperty("creditors")]
        public Creditor Creditor { get; set; }
    }
}