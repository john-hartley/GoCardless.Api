using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class CustomerBankAccountsClient : ICustomerBankAccountsClient
    {
        private readonly ApiClient _apiClient;

        public CustomerBankAccountsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                request =>
                {
                    return request
                        .AppendPathSegment("customer_bank_accounts")
                        .PostJsonAsync(new { customer_bank_accounts = options })
                        .ReceiveJson<Response<CustomerBankAccount>>();
                });
        }

        public async Task<Response<CustomerBankAccount>> DisableAsync(DisableCustomerBankAccountOptions options)
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
                    .AppendPathSegment($"customer_bank_accounts/{options.Id}/actions/disable")
                    .PostJsonAsync(new { })
                    .ReceiveJson<Response<CustomerBankAccount>>();
            });
        }

        public async Task<Response<CustomerBankAccount>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"customer_bank_accounts/{id}")
                    .GetJsonAsync<Response<CustomerBankAccount>>();
            });
        }

        public async Task<PagedResponse<CustomerBankAccount>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("customer_bank_accounts")
                    .GetJsonAsync<PagedResponse<CustomerBankAccount>>();
            });
        }

        public async Task<PagedResponse<CustomerBankAccount>> GetPageAsync(GetCustomerBankAccountsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("customer_bank_accounts")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<CustomerBankAccount>>();
            });
        }

        public IPager<GetCustomerBankAccountsOptions, CustomerBankAccount> PageUsing(GetCustomerBankAccountsOptions options)
        {
            return new Pager<GetCustomerBankAccountsOptions, CustomerBankAccount>(GetPageAsync, options);
        }

        public async Task<Response<CustomerBankAccount>> UpdateAsync(UpdateCustomerBankAccountOptions options)
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
                    .AppendPathSegment($"customer_bank_accounts/{options.Id}")
                    .PutJsonAsync(new { customer_bank_accounts = options })
                    .ReceiveJson<Response<CustomerBankAccount>>();
            });
        }
    }
}