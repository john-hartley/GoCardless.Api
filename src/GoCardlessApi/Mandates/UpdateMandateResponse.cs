using Newtonsoft.Json;

namespace GoCardlessApi.Mandates
{
    public class UpdateMandateResponse
    {
        [JsonProperty("mandates")]
        public Mandate Mandate { get; set; }
    }
}