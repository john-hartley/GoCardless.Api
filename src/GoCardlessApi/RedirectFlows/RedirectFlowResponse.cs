using Newtonsoft.Json;

namespace GoCardlessApi.RedirectFlows
{
    public class RedirectFlowResponse
    {
        [JsonProperty("redirect_flows")]
        public RedirectFlow RedirectFlow { get; set; }
    }
}