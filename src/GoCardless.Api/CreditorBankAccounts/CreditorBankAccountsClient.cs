using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class CreditorBankAccountsClient : ApiClientBase, ICreditorBankAccountsClient
    {
        public CreditorBankAccountsClient(ClientConfiguration configuration) : base(configuration) { }

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

        public Task<Response<CreditorBankAccount>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return GetAsync<Response<CreditorBankAccount>>($"creditor_bank_accounts/{id}");
        }

        public Task<PagedResponse<CreditorBankAccount>> GetPageAsync()
        {
            return GetAsync<PagedResponse<CreditorBankAccount>>("creditor_bank_accounts");
        }

        public Task<PagedResponse<CreditorBankAccount>> GetPageAsync(GetCreditorBankAccountsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<CreditorBankAccount>>(
                "creditor_bank_accounts",
                request.ToReadOnlyDictionary()
            );
        }
    }
}