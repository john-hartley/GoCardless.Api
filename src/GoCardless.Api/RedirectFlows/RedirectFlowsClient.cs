using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.RedirectFlows
{
    public class RedirectFlowsClient : ApiClientBase, IRedirectFlowsClient
    {
        public RedirectFlowsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<Response<RedirectFlow>> CompleteAsync(CompleteRedirectFlowRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<RedirectFlow>>(
                $"redirect_flows/{request.Id}/actions/complete",
                new { data = request }
            );
        }

        public Task<Response<RedirectFlow>> CreateAsync(CreateRedirectFlowRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<RedirectFlow>>(
                "redirect_flows", 
                new { redirect_flows = request }
            );
        }

        public Task<Response<RedirectFlow>> ForIdAsync(string redirectFlowId)
        {
            if (string.IsNullOrWhiteSpace(redirectFlowId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(redirectFlowId));
            }

            return GetAsync<Response<RedirectFlow>>($"redirect_flows/{redirectFlowId}");
        }
    }
}