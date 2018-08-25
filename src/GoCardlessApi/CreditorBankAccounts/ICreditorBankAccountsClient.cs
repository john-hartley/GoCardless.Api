using System.Threading.Tasks;

namespace GoCardlessApi.CreditorBankAccounts
{
    public interface ICreditorBankAccountsClient
    {
        Task<CreateCreditorBankAccountResponse> CreateAsync(CreateCreditorBankAccountRequest request);
        Task<AllCreditorBankAccountsResponse> AllAsync();
        Task<CreditorBankAccountResponse> ForIdAsync(string creditorBankAccountId);
        Task<DisableCreditorBankAccountResponse> DisableAsync(DisableCreditorBankAccountRequest request);
    }
}