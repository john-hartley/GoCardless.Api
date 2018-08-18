using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class Links
    {
        [JsonProperty("mandate")]
        public string Mandate { get; set; }
    }
}
