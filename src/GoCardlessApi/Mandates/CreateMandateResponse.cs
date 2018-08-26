using Newtonsoft.Json;

namespace GoCardlessApi.Mandates
{
    public class CreateMandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}