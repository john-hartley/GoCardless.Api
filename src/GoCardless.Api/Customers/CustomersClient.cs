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
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetCustomersRequest, Customer> BuildPager()
        {
            return new Pager<GetCustomersRequest, Customer>(GetPageAsync);
        }

        public async Task<Response<Customer>> CreateAsync(CreateCustomerRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Customer>>(
                "customers",
                new { customers = options },
                request =>
                {
                    request
                        .AppendPathSegment("customers")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                });
        }

        public async Task<Response<Customer>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Customer>>(request =>
            {
                request.AppendPathSegment($"customers/{id}");
            });
        }

        public async Task<PagedResponse<Customer>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Customer>>(request =>
            {
                request.AppendPathSegment("customers");
            });
        }

        public async Task<PagedResponse<Customer>> GetPageAsync(GetCustomersRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Customer>>(request =>
            {
                request
                    .AppendPathSegment("customers")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public async Task<Response<Customer>> UpdateAsync(UpdateCustomerRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PutAsync<Response<Customer>>(
                new { customers = options },
                request =>
                {
                    request.AppendPathSegment($"customers/{options.Id}");
                });
        }
    }
}