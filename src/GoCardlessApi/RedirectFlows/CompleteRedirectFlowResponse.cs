using Newtonsoft.Json;

namespace GoCardlessApi.RedirectFlows
{
    public class CompleteRedirectFlowResponse
    {
        [JsonProperty("redirect_flows")]
        public RedirectFlow RedirectFlow { get; set; }
    }
}