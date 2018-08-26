using System.Threading.Tasks;

namespace GoCardlessApi.CustomerBankAccounts
{
    public interface ICustomerBankAccountsClient
    {
        Task<AllCustomerBankAccountsResponse> AllAsync();
        Task<CustomerBankAccountResponse> CreateAsync(CreateCustomerBankAccountRequest request);
        Task<DisableCustomerBankAccountResponse> DisableAsync(DisableCustomerBankAccountRequest request);
        Task<CustomerBankAccountResponse> ForIdAsync(string customerBankAccountId);
        Task<UpdateCustomerBankAccountResponse> UpdateAsync(UpdateCustomerBankAccountRequest request);
    }
}