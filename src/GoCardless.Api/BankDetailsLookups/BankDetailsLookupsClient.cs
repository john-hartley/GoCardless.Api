﻿using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.BankDetailsLookups
{
    public class BankDetailsLookupsClient : IBankDetailsLookupsClient
    {
        private readonly ApiClient _apiClient;

        public BankDetailsLookupsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<BankDetailsLookup>> LookupAsync(BankDetailsLookupOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("bank_details_lookups")
                    .PostJsonAsync(new { bank_details_lookups = options })
                    .ReceiveJson<Response<BankDetailsLookup>>();
            });
        }
    }
}