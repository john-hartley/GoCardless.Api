using GoCardless.Api.Core;
using GoCardless.Api.Core.Paging;
using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public interface ICustomersClient
    {
        IPagerBuilder<GetCustomersRequest, Customer> BuildPager();
        Task<Response<Customer>> CreateAsync(CreateCustomerRequest request);
        Task<Response<Customer>> ForIdAsync(string customerId);
        Task<PagedResponse<Customer>> GetPageAsync();
        Task<PagedResponse<Customer>> GetPageAsync(GetCustomersRequest request);
        Task<Response<Customer>> UpdateAsync(UpdateCustomerRequest request);
    }
}