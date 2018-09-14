using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public interface ICreditorBankAccountsClient
    {
        Task<AllCreditorBankAccountsResponse> AllAsync();
        Task<AllCreditorBankAccountsResponse> AllAsync(AllCreditorBankAccountsRequest request);
        Task<CreditorBankAccountResponse> CreateAsync(CreateCreditorBankAccountRequest request);
        Task<CreditorBankAccountResponse> DisableAsync(DisableCreditorBankAccountRequest request);
        Task<CreditorBankAccountResponse> ForIdAsync(string creditorBankAccountId);
    }
}