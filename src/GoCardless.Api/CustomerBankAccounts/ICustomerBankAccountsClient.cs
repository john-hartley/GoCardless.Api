using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public interface ICustomerBankAccountsClient
    {
        IPagerBuilder<GetCustomerBankAccountsRequest, CustomerBankAccount> BuildPager();
        Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountRequest request);
        Task<Response<CustomerBankAccount>> DisableAsync(DisableCustomerBankAccountRequest request);
        Task<Response<CustomerBankAccount>> ForIdAsync(string id);
        Task<PagedResponse<CustomerBankAccount>> GetPageAsync();
        Task<PagedResponse<CustomerBankAccount>> GetPageAsync(GetCustomerBankAccountsRequest request);
        Task<Response<CustomerBankAccount>> UpdateAsync(UpdateCustomerBankAccountRequest request);
    }
}