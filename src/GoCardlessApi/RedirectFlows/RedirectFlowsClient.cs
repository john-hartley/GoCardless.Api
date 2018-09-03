using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.RedirectFlows
{
    public class RedirectFlowsClient : ApiClientBase, IRedirectFlowsClient
    {
        public RedirectFlowsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateRedirectFlowResponse> CreateAsync(CreateRedirectFlowRequest request)
        {
            return PostAsync<CreateRedirectFlowRequest, CreateRedirectFlowResponse>(
                "redirect_flows", 
                new { redirect_flows = request }
            );
        }

        public Task<RedirectFlowResponse> ForIdAsync(string redirectFlowId)
        {
            if (string.IsNullOrWhiteSpace(redirectFlowId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(redirectFlowId));
            }

            return GetAsync<RedirectFlowResponse>($"redirect_flows/{redirectFlowId}");
        }
    }
}