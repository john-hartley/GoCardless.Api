using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Customers
{
    public class CustomersClient : ICustomersClient
    {
        private readonly ApiClient _apiClient;

        public CustomersClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<Customer>> CreateAsync(CreateCustomerOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                async request =>
                {
                    return await request
                        .AppendPathSegment("customers")
                        .PostJsonAsync(new { customers = options })
                        .ReceiveJson<Response<Customer>>();
                });
        }

        public async Task<Response<Customer>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"customers/{id}")
                    .GetJsonAsync<Response<Customer>>();
            });
        }

        public async Task<PagedResponse<Customer>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("customers")
                    .GetJsonAsync<PagedResponse<Customer>>();
            });
        }

        public async Task<PagedResponse<Customer>> GetPageAsync(GetCustomersOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("customers")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Customer>>();
            });
        }

        public IPager<GetCustomersOptions, Customer> PageUsing(GetCustomersOptions options)
        {
            return new Pager<GetCustomersOptions, Customer>(GetPageAsync, options);
        }

        public async Task<Response<Customer>> UpdateAsync(UpdateCustomerOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"customers/{options.Id}")
                    .PutJsonAsync(new { customers = options })
                    .ReceiveJson<Response<Customer>>();
            });
        }
    }
}