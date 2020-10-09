using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class CreditorBankAccountsClient : ApiClient, ICreditorBankAccountsClient
    {
        private readonly IApiClient _apiClient;

        public CreditorBankAccountsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetCreditorBankAccountsRequest, CreditorBankAccount> BuildPager()
        {
            return new Pager<GetCreditorBankAccountsRequest, CreditorBankAccount>(GetPageAsync);
        }

        public Task<Response<CreditorBankAccount>> CreateAsync(CreateCreditorBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<CreditorBankAccount>>(
                "creditor_bank_accounts",
                new { creditor_bank_accounts = request },
                request.IdempotencyKey
            );
        }

        public Task<Response<CreditorBankAccount>> DisableAsync(DisableCreditorBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<CreditorBankAccount>>(
                $"creditor_bank_accounts/{request.Id}/actions/disable"
            );
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

        public async Task<PagedResponse<CreditorBankAccount>> GetPageAsync(GetCreditorBankAccountsRequest options)
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
    }
}