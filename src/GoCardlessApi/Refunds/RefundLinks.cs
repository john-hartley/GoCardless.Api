using Newtonsoft.Json;

namespace GoCardlessApi.Refunds
{
    public class RefundLinks
    {
        [JsonProperty("payment")]
        public string Payment { get; set; }
    }
}