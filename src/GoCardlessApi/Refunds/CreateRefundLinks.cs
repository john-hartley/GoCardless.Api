using Newtonsoft.Json;

namespace GoCardlessApi.Refunds
{
    public class CreateRefundLinks
    {
        [JsonProperty("payment")]
        public string Payment { get; set; }
    }
}