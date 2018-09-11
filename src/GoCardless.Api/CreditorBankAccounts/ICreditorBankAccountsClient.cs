using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public interface ICreditorBankAccountsClient
    {
        Task<AllCreditorBankAccountsResponse> AllAsync();
        Task<AllCreditorBankAccountsResponse> AllAsync(AllCreditorBankAccountsRequest request);
        Task<CreateCreditorBankAccountResponse> CreateAsync(CreateCreditorBankAccountRequest request);
        Task<DisableCreditorBankAccountResponse> DisableAsync(DisableCreditorBankAccountRequest request);
        Task<CreditorBankAccountResponse> ForIdAsync(string creditorBankAccountId);
    }
}