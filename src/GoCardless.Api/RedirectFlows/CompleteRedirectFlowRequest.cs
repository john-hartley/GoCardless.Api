using Newtonsoft.Json;

namespace GoCardless.Api.RedirectFlows
{
    public class CompleteRedirectFlowRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string SessionToken { get; set; }
    }
}