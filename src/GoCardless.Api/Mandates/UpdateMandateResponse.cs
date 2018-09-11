using Newtonsoft.Json;

namespace GoCardless.Api.Mandates
{
    public class UpdateMandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}