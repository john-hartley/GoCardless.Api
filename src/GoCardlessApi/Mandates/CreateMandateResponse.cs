using Newtonsoft.Json;

namespace GoCardless.Api.Mandates
{
    public class CreateMandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}