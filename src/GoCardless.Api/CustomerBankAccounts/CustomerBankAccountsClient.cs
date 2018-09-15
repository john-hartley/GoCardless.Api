using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class CustomerBankAccountsClient : ApiClientBase, ICustomerBankAccountsClient
    {
        public CustomerBankAccountsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<CustomerBankAccount>> AllAsync()
        {
            return GetAsync<PagedResponse<CustomerBankAccount>>("customer_bank_accounts");
        }

        public Task<PagedResponse<CustomerBankAccount>> AllAsync(AllCustomerBankAccountsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<CustomerBankAccount>>(
                "customer_bank_accounts",
                request.ToReadOnlyDictionary()
            );
        }

        public Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<CustomerBankAccount>>(
                "customer_bank_accounts",
                new { customer_bank_accounts = request },
                request.IdempotencyKey
            );
        }

        public Task<Response<CustomerBankAccount>> DisableAsync(DisableCustomerBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<CustomerBankAccount>>(
                $"customer_bank_accounts/{request.Id}/actions/disable"
            );
        }

        public Task<Response<CustomerBankAccount>> ForIdAsync(string customerBankAccountId)
        {
            if (string.IsNullOrWhiteSpace(customerBankAccountId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(customerBankAccountId));
            }
            
            return GetAsync<Response<CustomerBankAccount>>($"customer_bank_accounts/{customerBankAccountId}");
        }

        public Task<Response<CustomerBankAccount>> UpdateAsync(UpdateCustomerBankAccountRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<CustomerBankAccount>>(
                $"customer_bank_accounts/{request.Id}",
                new { customer_bank_accounts = request }
            );
        }
    }
}