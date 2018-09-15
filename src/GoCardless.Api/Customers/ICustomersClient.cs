using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public interface ICustomersClient
    {
        Task<PagedResponse<Customer>> AllAsync();
        Task<PagedResponse<Customer>> AllAsync(AllCustomersRequest request);
        Task<CustomerResponse> CreateAsync(CreateCustomerRequest request);
        Task<CustomerResponse> ForIdAsync(string customerId);
        Task<CustomerResponse> UpdateAsync(UpdateCustomerRequest request);
    }
}