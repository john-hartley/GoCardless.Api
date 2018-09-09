using Newtonsoft.Json;

namespace GoCardless.Api.RedirectFlows
{
    public class CreateRedirectFlowResponse
    {
        [JsonProperty("redirect_flows")]
        public RedirectFlow RedirectFlow { get; set; }
    }
}