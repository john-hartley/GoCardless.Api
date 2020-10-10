using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class CustomerBankAccountsClient : ICustomerBankAccountsClient
    {
        private readonly IApiClient _apiClient;

        public CustomerBankAccountsClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetCustomerBankAccountsOptions, CustomerBankAccount> BuildPager()
        {
            return new Pager<GetCustomerBankAccountsOptions, CustomerBankAccount>(GetPageAsync);
        }

        public async Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<CustomerBankAccount>>(
                request =>
                {
                    request
                        .AppendPathSegment("customer_bank_accounts")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                },
                new { customer_bank_accounts = options });
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

            return await _apiClient.PostAsync<Response<CustomerBankAccount>>(
                request =>
                {
                    request.AppendPathSegment($"customer_bank_accounts/{options.Id}/actions/disable");
                });
        }

        public async Task<Response<CustomerBankAccount>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<CustomerBankAccount>>(request =>
            {
                request.AppendPathSegment($"customer_bank_accounts/{id}");
            });
        }

        public async Task<PagedResponse<CustomerBankAccount>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<CustomerBankAccount>>(request =>
            {
                request.AppendPathSegment("customer_bank_accounts");
            });
        }

        public async Task<PagedResponse<CustomerBankAccount>> GetPageAsync(GetCustomerBankAccountsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<CustomerBankAccount>>(request =>
            {
                request
                    .AppendPathSegment("customer_bank_accounts")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
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

            return await _apiClient.PutAsync<Response<CustomerBankAccount>>(
                request =>
                {
                    request.AppendPathSegment($"customer_bank_accounts/{options.Id}");
                },
                new { customer_bank_accounts = options });
        }
    }
}