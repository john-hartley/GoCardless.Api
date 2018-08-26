using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class CustomerBankAccountsClient : ApiClientBase
    {
        public CustomerBankAccountsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CustomerBankAccountResponse> CreateAsync(CreateCustomerBankAccountRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateCustomerBankAccountRequest, CustomerBankAccountResponse>(
                new { customer_bank_accounts = request },
                idempotencyKey,
                new string[] { "customer_bank_accounts" }
            );
        }

        public Task<DisableCustomerBankAccountResponse> DisableAsync(DisableCustomerBankAccountRequest request)
        {
            return PostAsync<DisableCustomerBankAccountRequest, DisableCustomerBankAccountResponse>(
                new string[] { "customer_bank_accounts", request.Id, "actions", "disable" }
            );
        }

        public Task<AllCustomerBankAccountsResponse> AllAsync()
        {
            return GetAsync<AllCustomerBankAccountsResponse>(
                new string[] { "customer_bank_accounts" }
            );
        }

        public Task<CustomerBankAccountResponse> ForIdAsync(string customerBankAccountId)
        {
            return GetAsync<CustomerBankAccountResponse>("customer_bank_accounts", customerBankAccountId);
        }
    }
}