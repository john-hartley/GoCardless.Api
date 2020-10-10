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

            return await _apiClient.PostAsync<Response<CreditorBankAccount>>(
                request =>
                {
                    request
                        .AppendPathSegment("creditor_bank_accounts")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                },
                new { creditor_bank_accounts = options });
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

            return await _apiClient.PostAsync<Response<CreditorBankAccount>>(
                request =>
                {
                    request.AppendPathSegment($"creditor_bank_accounts/{options.Id}/actions/disable");
                },
                new { creditor_bank_accounts = options });
        }

        public async Task<Response<CreditorBankAccount>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<CreditorBankAccount>>(request =>
            {
                request.AppendPathSegment($"creditor_bank_accounts/{id}");
            });
        }

        public async Task<PagedResponse<CreditorBankAccount>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<CreditorBankAccount>>(request =>
            {
                request.AppendPathSegment("creditor_bank_accounts");
            });
        }

        public async Task<PagedResponse<CreditorBankAccount>> GetPageAsync(GetCreditorBankAccountsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<CreditorBankAccount>>(request =>
            {
                request
                    .AppendPathSegment("creditor_bank_accounts")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public IPager<GetCreditorBankAccountsOptions, CreditorBankAccount> PageFrom(GetCreditorBankAccountsOptions options)
        {
            return new Pager<GetCreditorBankAccountsOptions, CreditorBankAccount>(GetPageAsync, options);
        }
    }
}