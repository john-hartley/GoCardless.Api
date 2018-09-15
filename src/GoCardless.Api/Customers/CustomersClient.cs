using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public class CustomersClient : ApiClientBase, ICustomersClient
    {
        public CustomersClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<Customer>> AllAsync()
        {
            return GetAsync<PagedResponse<Customer>>("customers");
        }

        public Task<PagedResponse<Customer>> AllAsync(AllCustomersRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Customer>>("customers", request.ToReadOnlyDictionary());
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

        public Task<Response<Customer>> ForIdAsync(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(customerId));
            }

            return GetAsync<Response<Customer>>($"customers/{customerId}");
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