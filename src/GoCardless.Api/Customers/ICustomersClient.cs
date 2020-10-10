using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Customers
{
    public interface ICustomersClient
    {
        IPagerBuilder<GetCustomersOptions, Customer> BuildPager();
        Task<Response<Customer>> CreateAsync(CreateCustomerOptions options);
        Task<Response<Customer>> ForIdAsync(string id);
        Task<PagedResponse<Customer>> GetPageAsync();
        Task<PagedResponse<Customer>> GetPageAsync(GetCustomersOptions options);
        Task<Response<Customer>> UpdateAsync(UpdateCustomerOptions options);
    }
}