using Newtonsoft.Json;

namespace GoCardlessApi.Creditors
{
    public class UpdateCreditorResponse
    {
        [JsonProperty("creditors")]
        public Creditor Creditor { get; set; }
    }
}