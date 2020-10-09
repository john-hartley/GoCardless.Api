using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.RedirectFlows
{
    public class RedirectFlowsClient : ApiClient, IRedirectFlowsClient
    {
        private readonly IApiClient _apiClient;

        public RedirectFlowsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

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

        public async Task<Response<RedirectFlow>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<RedirectFlow>>(request =>
            {
                request.AppendPathSegment($"redirect_flows/{id}");
            });
        }
    }
}