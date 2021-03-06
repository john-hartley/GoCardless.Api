﻿using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Creditors
{
    public class CreditorsClient : ICreditorsClient
    {
        private readonly ApiClient _apiClient;

        public CreditorsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<Creditor>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"creditors/{id}")
                    .GetJsonAsync<Response<Creditor>>();
            });
        }

        public async Task<PagedResponse<Creditor>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("creditors")
                    .GetJsonAsync<PagedResponse<Creditor>>();
            });
        }

        public async Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("creditors")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Creditor>>();
            });
        }

        public IPager<GetCreditorsOptions, Creditor> PageUsing(GetCreditorsOptions options)
        {
            return new Pager<GetCreditorsOptions, Creditor>(GetPageAsync, options);
        }

        public async Task<Response<Creditor>> UpdateAsync(UpdateCreditorOptions options)
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
                    .AppendPathSegment($"creditors/{options.Id}")
                    .PutJsonAsync(new { creditors = options })
                    .ReceiveJson<Response<Creditor>>();
            });
        }
    }
}