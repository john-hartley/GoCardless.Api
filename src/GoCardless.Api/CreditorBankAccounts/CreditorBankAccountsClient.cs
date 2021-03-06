﻿using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class CreditorBankAccountsClient : ICreditorBankAccountsClient
    {
        private readonly ApiClient _apiClient;

        public CreditorBankAccountsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<CreditorBankAccount>> CreateAsync(CreateCreditorBankAccountOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                async request =>
                {
                    return await request
                        .AppendPathSegment("creditor_bank_accounts")
                        .PostJsonAsync(new { creditor_bank_accounts = options })
                        .ReceiveJson<Response<CreditorBankAccount>>();
                });
        }

        public async Task<Response<CreditorBankAccount>> DisableAsync(DisableCreditorBankAccountOptions options)
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
                    .AppendPathSegment($"creditor_bank_accounts/{options.Id}/actions/disable")
                    .PostJsonAsync(new { })
                    .ReceiveJson<Response<CreditorBankAccount>>();
            });
        }

        public async Task<Response<CreditorBankAccount>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"creditor_bank_accounts/{id}")
                    .GetJsonAsync<Response<CreditorBankAccount>>();
            });
        }

        public async Task<PagedResponse<CreditorBankAccount>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("creditor_bank_accounts")
                    .GetJsonAsync<PagedResponse<CreditorBankAccount>>();
            });
        }

        public async Task<PagedResponse<CreditorBankAccount>> GetPageAsync(GetCreditorBankAccountsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("creditor_bank_accounts")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<CreditorBankAccount>>();
            });
        }

        public IPager<GetCreditorBankAccountsOptions, CreditorBankAccount> PageUsing(GetCreditorBankAccountsOptions options)
        {
            return new Pager<GetCreditorBankAccountsOptions, CreditorBankAccount>(GetPageAsync, options);
        }
    }
}