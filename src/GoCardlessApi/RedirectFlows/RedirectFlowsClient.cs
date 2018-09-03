using GoCardlessApi.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoCardlessApi.RedirectFlows
{
    public class RedirectFlowsClient : ApiClientBase
    {
        public RedirectFlowsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateRedirectFlowResponse> CreateAsync(CreateRedirectFlowRequest request)
        {
            return PostAsync<CreateRedirectFlowRequest, CreateRedirectFlowResponse>(
                "redirect_flows", 
                new { redirect_flows = request }
            );
        }
    }
}