using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class CustomerBankAccountsClient : ApiClient, ICustomerBankAccountsClient
    {
        private readonly IApiClient _apiClient;

        public CustomerBankAccountsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetCustomerBankAccountsRequest, CustomerBankAccount> BuildPager()
        {
            return new Pager<GetCustomerBankAccountsRequest, CustomerBankAccount>(GetPageAsync);
        }

        public async Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<CustomerBankAccount>>(
                "customer_bank_accounts",
                new { customer_bank_accounts = options },
                request =>
                {
                    request
                        .AppendPathSegment("customer_bank_accounts")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                });
        }

        public async Task<Response<CustomerBankAccount>> DisableAsync(DisableCustomerBankAccountRequest options)
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
                "customer_bank_accounts",
                null,
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

        public async Task<PagedResponse<CustomerBankAccount>> GetPageAsync(GetCustomerBankAccountsRequest options)
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

        public async Task<Response<CustomerBankAccount>> UpdateAsync(UpdateCustomerBankAccountRequest options)
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
                new { customer_bank_accounts = options },
                request =>
                {
                    request.AppendPathSegment($"customer_bank_accounts/{options.Id}");
                });
        }
    }
}