using GoCardless.Api.Core;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class CreditorBankAccountsClient : ApiClientBase, ICreditorBankAccountsClient
    {
        public CreditorBankAccountsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllCreditorBankAccountsResponse> AllAsync()
        {
            return GetAsync<AllCreditorBankAccountsResponse>("creditor_bank_accounts");
        }

        public Task<AllCreditorBankAccountsResponse> AllAsync(AllCreditorBankAccountsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllCreditorBankAccountsResponse>("creditor_bank_accounts", request.ToReadOnlyDictionary());
        }

        public Task<CreateCreditorBankAccountResponse> CreateAsync(CreateCreditorBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<CreateCreditorBankAccountResponse>(
                "creditor_bank_accounts",
                new { creditor_bank_accounts = request },
                request.IdempotencyKey
            );
        }

        public Task<DisableCreditorBankAccountResponse> DisableAsync(DisableCreditorBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<DisableCreditorBankAccountResponse>(
                $"creditor_bank_accounts/{request.Id}/actions/disable"
            );
        }

        public Task<CreditorBankAccountResponse> ForIdAsync(string creditorBankAccountId)
        {
            if (string.IsNullOrWhiteSpace(creditorBankAccountId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(creditorBankAccountId));
            }

            return GetAsync<CreditorBankAccountResponse>($"creditor_bank_accounts/{creditorBankAccountId}");
        }
    }
}