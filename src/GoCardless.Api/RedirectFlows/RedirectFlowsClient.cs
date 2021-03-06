﻿using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.RedirectFlows
{
    public class RedirectFlowsClient : IRedirectFlowsClient
    {
        private readonly ApiClient _apiClient;

        public RedirectFlowsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
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

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
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

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
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

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"redirect_flows/{id}")
                    .GetJsonAsync<Response<RedirectFlow>>();
            });
        }
    }
}