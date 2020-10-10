using Newtonsoft.Json;

namespace GoCardless.Api.RedirectFlows
{
    public class CompleteRedirectFlowOptions
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string SessionToken { get; set; }
    }
}