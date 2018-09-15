using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public interface ICustomerBankAccountsClient
    {
        Task<PagedResponse<CustomerBankAccount>> AllAsync();
        Task<PagedResponse<CustomerBankAccount>> AllAsync(AllCustomerBankAccountsRequest request);
        Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountRequest request);
        Task<Response<CustomerBankAccount>> DisableAsync(DisableCustomerBankAccountRequest request);
        Task<Response<CustomerBankAccount>> ForIdAsync(string customerBankAccountId);
        Task<Response<CustomerBankAccount>> UpdateAsync(UpdateCustomerBankAccountRequest request);
    }
}