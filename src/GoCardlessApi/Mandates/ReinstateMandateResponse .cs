using Newtonsoft.Json;

namespace GoCardlessApi.Mandates
{
    public class ReinstateMandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}