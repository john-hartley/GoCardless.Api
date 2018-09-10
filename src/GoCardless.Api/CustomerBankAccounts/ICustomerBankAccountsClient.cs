using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public interface ICustomerBankAccountsClient
    {
        Task<AllCustomerBankAccountsResponse> AllAsync();
        Task<AllCustomerBankAccountsResponse> AllAsync(AllCustomerBankAccountsRequest request);
        Task<CustomerBankAccountResponse> CreateAsync(CreateCustomerBankAccountRequest request);
        Task<DisableCustomerBankAccountResponse> DisableAsync(DisableCustomerBankAccountRequest request);
        Task<CustomerBankAccountResponse> ForIdAsync(string customerBankAccountId);
        Task<UpdateCustomerBankAccountResponse> UpdateAsync(UpdateCustomerBankAccountRequest request);
    }
}