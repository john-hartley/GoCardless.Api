using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class CreditorResponse
    {
        [JsonProperty("creditors")]
        public Creditor Creditor { get; set; }
    }
}