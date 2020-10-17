using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class CreditorBankAccountsClient : ICreditorBankAccountsClient
    {
        private readonly IApiClient _apiClient;

        public CreditorBankAccountsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public CreditorBankAccountsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<CreditorBankAccount>> CreateAsync(CreateCreditorBankAccountOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentAsync(
                options.IdempotencyKey,
                request =>
                {
                    return request
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"creditor_bank_accounts/{id}")
                    .GetJsonAsync<Response<CreditorBankAccount>>();
            });
        }

        public async Task<PagedResponse<CreditorBankAccount>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("creditor_bank_accounts")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<CreditorBankAccount>>();
            });
        }

        public IPager<GetCreditorBankAccountsOptions, CreditorBankAccount> PageFrom(GetCreditorBankAccountsOptions options)
        {
            return new Pager<GetCreditorBankAccountsOptions, CreditorBankAccount>(GetPageAsync, options);
        }
    }
}