using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.RedirectFlows
{
    public class RedirectFlowsClient : IRedirectFlowsClient
    {
        private readonly IApiClient _apiClient;

        public RedirectFlowsClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Response<RedirectFlow>> CompleteAsync(CompleteRedirectFlowRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PostAsync<Response<RedirectFlow>>(
                "redirect_flows",
                request =>
                {
                    request.AppendPathSegment($"redirect_flows/{options.Id}/actions/complete");
                },
                new { data = options });
        }

        public async Task<Response<RedirectFlow>> CreateAsync(CreateRedirectFlowRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<RedirectFlow>>(
                "redirect_flows",
                request =>
                {
                    request.AppendPathSegment("redirect_flows");
                },
                new { redirect_flows = options });
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