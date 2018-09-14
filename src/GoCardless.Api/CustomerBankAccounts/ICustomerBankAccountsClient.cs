using System.Threading.Tasks;

namespace GoCardless.Api.CustomerBankAccounts
{
    public interface ICustomerBankAccountsClient
    {
        Task<AllCustomerBankAccountsResponse> AllAsync();
        Task<AllCustomerBankAccountsResponse> AllAsync(AllCustomerBankAccountsRequest request);
        Task<CustomerBankAccountResponse> CreateAsync(CreateCustomerBankAccountRequest request);
        Task<CustomerBankAccountResponse> DisableAsync(DisableCustomerBankAccountRequest request);
        Task<CustomerBankAccountResponse> ForIdAsync(string customerBankAccountId);
        Task<CustomerBankAccountResponse> UpdateAsync(UpdateCustomerBankAccountRequest request);
    }
}