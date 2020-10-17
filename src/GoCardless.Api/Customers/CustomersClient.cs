using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public class CustomersClient : ICustomersClient
    {
        private readonly IApiClient _apiClient;

        public CustomersClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public CustomersClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<Customer>> CreateAsync(CreateCustomerOptions options)
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"customers/{id}")
                    .GetJsonAsync<Response<Customer>>();
            });
        }

        public async Task<PagedResponse<Customer>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("customers")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Customer>>();
            });
        }

        public IPager<GetCustomersOptions, Customer> PageFrom(GetCustomersOptions options)
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"customers/{options.Id}")
                    .PutJsonAsync(new { customers = options })
                    .ReceiveJson<Response<Customer>>();
            });
        }
    }
}