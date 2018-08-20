using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class UpdateCreditorResponse
    {
        [JsonProperty("creditors")]
        public Creditor Creditor { get; set; }
    }
}