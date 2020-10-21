using Newtonsoft.Json;

namespace GoCardlessApi.RedirectFlows
{
    public class CompleteRedirectFlowOptions
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string SessionToken { get; set; }
    }
}