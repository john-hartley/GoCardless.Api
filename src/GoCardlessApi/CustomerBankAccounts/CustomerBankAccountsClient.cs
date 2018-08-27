using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class CustomerBankAccountsClient : ApiClientBase, ICustomerBankAccountsClient
    {
        public CustomerBankAccountsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CustomerBankAccountResponse> CreateAsync(CreateCustomerBankAccountRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateCustomerBankAccountRequest, CustomerBankAccountResponse>(
                "customer_bank_accounts",
                new { customer_bank_accounts = request },
                idempotencyKey
            );
        }

        public Task<DisableCustomerBankAccountResponse> DisableAsync(DisableCustomerBankAccountRequest request)
        {
            return PostAsync<DisableCustomerBankAccountRequest, DisableCustomerBankAccountResponse>(
                $"customer_bank_accounts/{request.Id}/actions/disable"
            );
        }

        public Task<AllCustomerBankAccountsResponse> AllAsync()
        {
            return GetAsync<AllCustomerBankAccountsResponse>("customer_bank_accounts");
        }

        public Task<CustomerBankAccountResponse> ForIdAsync(string customerBankAccountId)
        {
            return GetAsync<CustomerBankAccountResponse>($"customer_bank_accounts/{customerBankAccountId}");
        }

        public Task<UpdateCustomerBankAccountResponse> UpdateAsync(UpdateCustomerBankAccountRequest request)
        {
            return PutAsync<UpdateCustomerBankAccountRequest, UpdateCustomerBankAccountResponse>(
                $"customer_bank_accounts/{request.Id}",
                new { customer_bank_accounts = request }
            );
        }
    }
}