using Newtonsoft.Json;

namespace GoCardlessApi.Mandates
{
    public class CancelMandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}