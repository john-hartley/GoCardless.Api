using Newtonsoft.Json;

namespace GoCardless.Api.RedirectFlows
{
    public class RedirectFlowResponse
    {
        [JsonProperty("redirect_flows")]
        public RedirectFlow RedirectFlow { get; set; }
    }
}