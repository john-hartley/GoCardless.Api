using Newtonsoft.Json;

namespace GoCardlessApi.RedirectFlows
{
    public class CompleteRedirectFlowRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string SessionToken { get; set; }
    }
}