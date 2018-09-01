using System.Threading.Tasks;

namespace GoCardlessApi.Customers
{
    public interface ICustomersClient
    {
        Task<CreateCustomerResponse> CreateAsync(CreateCustomerRequest request);
        Task<AllCustomersResponse> AllAsync();
        Task<AllCustomersResponse> AllAsync(AllCustomersRequest request);
        Task<CustomerResponse> ForIdAsync(string customerId);
        Task<UpdateCustomerResponse> UpdateAsync(UpdateCustomerRequest request);
    }
}