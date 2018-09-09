using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public interface ICustomersClient
    {
        Task<AllCustomersResponse> AllAsync();
        Task<AllCustomersResponse> AllAsync(AllCustomersRequest request);
        Task<CreateCustomerResponse> CreateAsync(CreateCustomerRequest request);
        Task<CustomerResponse> ForIdAsync(string customerId);
        Task<UpdateCustomerResponse> UpdateAsync(UpdateCustomerRequest request);
    }
}