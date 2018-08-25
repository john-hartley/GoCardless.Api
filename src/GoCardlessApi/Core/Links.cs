using Newtonsoft.Json;

namespace GoCardlessApi.Core
{
    public class Links
    {
        [JsonProperty("mandate")]
        public string Mandate { get; set; }
    }
}
