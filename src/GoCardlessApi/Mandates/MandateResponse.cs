using Newtonsoft.Json;

namespace GoCardless.Api.Mandates
{
    public class MandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}