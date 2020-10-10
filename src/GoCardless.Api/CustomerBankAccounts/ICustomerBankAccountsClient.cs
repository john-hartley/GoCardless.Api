using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public interface ICustomerBankAccountsClient
    {
        IPagerBuilder<GetCustomerBankAccountsOptions, CustomerBankAccount> BuildPager();
        Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountOptions request);
        Task<Response<CustomerBankAccount>> DisableAsync(DisableCustomerBankAccountOptions request);
        Task<Response<CustomerBankAccount>> ForIdAsync(string id);
        Task<PagedResponse<CustomerBankAccount>> GetPageAsync();
        Task<PagedResponse<CustomerBankAccount>> GetPageAsync(GetCustomerBankAccountsOptions request);
        Task<Response<CustomerBankAccount>> UpdateAsync(UpdateCustomerBankAccountOptions request);
    }
}