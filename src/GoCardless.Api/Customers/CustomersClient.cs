using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public class CustomersClient : ApiClientBase, ICustomersClient
    {
        public CustomersClient(ClientConfiguration configuration) : base(configuration) { }

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

        public Task<Response<Customer>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return GetAsync<Response<Customer>>($"customers/{id}");
        }

        public Task<PagedResponse<Customer>> GetPageAsync()
        {
            return GetAsync<PagedResponse<Customer>>("customers");
        }

        public Task<PagedResponse<Customer>> GetPageAsync(GetCustomersRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Customer>>("customers", request.ToReadOnlyDictionary());
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