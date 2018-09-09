using Newtonsoft.Json;

namespace GoCardless.Api.Mandates
{
    public class ReinstateMandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}