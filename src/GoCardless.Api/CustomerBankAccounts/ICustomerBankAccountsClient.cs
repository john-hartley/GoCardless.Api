using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public interface ICustomerBankAccountsClient
    {
        IPagerBuilder<GetCustomerBankAccountsOptions, CustomerBankAccount> BuildPager();
        Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountOptions options);
        Task<Response<CustomerBankAccount>> DisableAsync(DisableCustomerBankAccountOptions options);
        Task<Response<CustomerBankAccount>> ForIdAsync(string id);
        Task<PagedResponse<CustomerBankAccount>> GetPageAsync();
        Task<PagedResponse<CustomerBankAccount>> GetPageAsync(GetCustomerBankAccountsOptions options);
        Task<Response<CustomerBankAccount>> UpdateAsync(UpdateCustomerBankAccountOptions options);
    }
}