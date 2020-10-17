using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Customers
{
    public interface ICustomersClient : IPageable<GetCustomersOptions, Customer>
    {
        Task<Response<Customer>> CreateAsync(CreateCustomerOptions options);
        Task<Response<Customer>> ForIdAsync(string id);
        Task<PagedResponse<Customer>> GetPageAsync();
        Task<PagedResponse<Customer>> GetPageAsync(GetCustomersOptions options);
        Task<Response<Customer>> UpdateAsync(UpdateCustomerOptions options);
    }
}