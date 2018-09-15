using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public interface ICustomersClient
    {
        Task<PagedResponse<Customer>> AllAsync();
        Task<PagedResponse<Customer>> AllAsync(AllCustomersRequest request);
        Task<Response<Customer>> CreateAsync(CreateCustomerRequest request);
        Task<Response<Customer>> ForIdAsync(string customerId);
        Task<Response<Customer>> UpdateAsync(UpdateCustomerRequest request);
    }
}