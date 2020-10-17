﻿using Flurl.Http;
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
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public RedirectFlowsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<RedirectFlow>> CompleteAsync(CompleteRedirectFlowOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"redirect_flows/{options.Id}/actions/complete")
                    .PostJsonAsync(new { data = options })
                    .ReceiveJson<Response<RedirectFlow>>();
            });
        }

        public async Task<Response<RedirectFlow>> CreateAsync(CreateRedirectFlowOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("redirect_flows")
                    .PostJsonAsync(new { redirect_flows = options })
                    .ReceiveJson<Response<RedirectFlow>>();
            });
        }

        public async Task<Response<RedirectFlow>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"redirect_flows/{id}")
                    .GetJsonAsync<Response<RedirectFlow>>();
            });
        }
    }
}