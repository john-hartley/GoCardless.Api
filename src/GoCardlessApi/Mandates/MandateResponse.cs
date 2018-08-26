using Newtonsoft.Json;

namespace GoCardlessApi.Mandates
{
    public class MandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}