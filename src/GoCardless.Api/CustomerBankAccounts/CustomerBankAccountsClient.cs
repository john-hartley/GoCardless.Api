using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class CustomerBankAccountsClient : ApiClientBase, ICustomerBankAccountsClient
    {
        public CustomerBankAccountsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllCustomerBankAccountsResponse> AllAsync()
        {
            return GetAsync<AllCustomerBankAccountsResponse>("customer_bank_accounts");
        }

        public Task<AllCustomerBankAccountsResponse> AllAsync(AllCustomerBankAccountsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllCustomerBankAccountsResponse>("customer_bank_accounts", request.ToReadOnlyDictionary());
        }

        public Task<CustomerBankAccountResponse> CreateAsync(CreateCustomerBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<CustomerBankAccountResponse>(
                "customer_bank_accounts",
                new { customer_bank_accounts = request },
                request.IdempotencyKey
            );
        }

        public Task<DisableCustomerBankAccountResponse> DisableAsync(DisableCustomerBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<DisableCustomerBankAccountResponse>(
                $"customer_bank_accounts/{request.Id}/actions/disable"
            );
        }

        public Task<CustomerBankAccountResponse> ForIdAsync(string customerBankAccountId)
        {
            if (string.IsNullOrWhiteSpace(customerBankAccountId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(customerBankAccountId));
            }
            
            return GetAsync<CustomerBankAccountResponse>($"customer_bank_accounts/{customerBankAccountId}");
        }

        public Task<UpdateCustomerBankAccountResponse> UpdateAsync(UpdateCustomerBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<UpdateCustomerBankAccountResponse>(
                $"customer_bank_accounts/{request.Id}",
                new { customer_bank_accounts = request }
            );
        }
    }
}