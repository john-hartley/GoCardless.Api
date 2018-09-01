using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Customers
{
    public class CustomersClient : ApiClientBase, ICustomersClient
    {
        public CustomersClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateCustomerResponse> CreateAsync(CreateCustomerRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateCustomerRequest, CreateCustomerResponse>(
                "customers",
                new { customers = request },
                idempotencyKey
            );
        }

        public Task<AllCustomersResponse> AllAsync()
        {
            return GetAsync<AllCustomersResponse>("customers");
        }

        public Task<AllCustomersResponse> AllAsync(AllCustomersRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllCustomersResponse>("customers", request.ToReadOnlyDictionary());
        }

        public Task<CustomerResponse> ForIdAsync(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(customerId));
            }

            return GetAsync<CustomerResponse>($"customers/{customerId}");
        }

        public Task<UpdateCustomerResponse> UpdateAsync(UpdateCustomerRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<UpdateCustomerRequest, UpdateCustomerResponse>(
                $"customers/{request.Id}",
                new { customers = request }
            );
        }
    }
}