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

        public Task<CustomerResponse> ForIdAsync(string customerId)
        {
            return GetAsync<CustomerResponse>($"customers/{customerId}");
        }

        public Task<UpdateCustomerResponse> UpdateAsync(UpdateCustomerRequest request)
        {
            return PutAsync<UpdateCustomerRequest, UpdateCustomerResponse>(
                $"customers/{request.Id}",
                new { customers = request }
            );
        }
    }
}