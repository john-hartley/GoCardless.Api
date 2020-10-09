using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public class CustomersClient : ApiClient, ICustomersClient
    {
        private readonly IApiClient _apiClient;

        public CustomersClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetCustomersRequest, Customer> BuildPager()
        {
            return new Pager<GetCustomersRequest, Customer>(GetPageAsync);
        }

        public Task<Response<Customer>> CreateAsync(CreateCustomerRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<Customer>>(
                "customers",
                new { customers = request },
                request.IdempotencyKey
            );
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

        public Task<Response<Customer>> UpdateAsync(UpdateCustomerRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<Customer>>(
                $"customers/{request.Id}",
                new { customers = request }
            );
        }
    }
}