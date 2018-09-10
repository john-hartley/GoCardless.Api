using Newtonsoft.Json;

namespace GoCardless.Api.RedirectFlows
{
    public class CompleteRedirectFlowResponse
    {
        [JsonProperty("redirect_flows")]
        public RedirectFlow RedirectFlow { get; set; }
    }
}