using Newtonsoft.Json;

namespace GoCardless.Api.Mandates
{
    public class CancelMandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}