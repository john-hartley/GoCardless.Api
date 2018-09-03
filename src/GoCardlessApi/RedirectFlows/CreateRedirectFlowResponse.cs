using Newtonsoft.Json;

namespace GoCardlessApi.RedirectFlows
{
    public class CreateRedirectFlowResponse
    {
        [JsonProperty("redirect_flows")]
        public RedirectFlow RedirectFlow { get; set; }
    }
}