using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public interface ICreditorBankAccountsClient
    {
        Task<PagedResponse<CreditorBankAccount>> AllAsync();
        Task<PagedResponse<CreditorBankAccount>> AllAsync(AllCreditorBankAccountsRequest request);
        Task<CreditorBankAccountResponse> CreateAsync(CreateCreditorBankAccountRequest request);
        Task<CreditorBankAccountResponse> DisableAsync(DisableCreditorBankAccountRequest request);
        Task<CreditorBankAccountResponse> ForIdAsync(string creditorBankAccountId);
    }
}