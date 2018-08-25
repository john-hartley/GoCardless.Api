using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Customers
{
    public class CustomersClient : ApiClientBase
    {
        public CustomersClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateCustomerResponse> CreateAsync(CreateCustomerRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateCustomerRequest, CreateCustomerResponse>(
                new { customers = request },
                idempotencyKey,
                new string[] { "customers" }
            );
        }
    }
}