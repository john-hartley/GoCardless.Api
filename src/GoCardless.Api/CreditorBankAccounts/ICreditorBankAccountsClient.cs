using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public interface ICreditorBankAccountsClient
    {
        Task<PagedResponse<CreditorBankAccount>> AllAsync();
        Task<PagedResponse<CreditorBankAccount>> AllAsync(AllCreditorBankAccountsRequest request);
        Task<Response<CreditorBankAccount>> CreateAsync(CreateCreditorBankAccountRequest request);
        Task<Response<CreditorBankAccount>> DisableAsync(DisableCreditorBankAccountRequest request);
        Task<Response<CreditorBankAccount>> ForIdAsync(string creditorBankAccountId);
    }
}