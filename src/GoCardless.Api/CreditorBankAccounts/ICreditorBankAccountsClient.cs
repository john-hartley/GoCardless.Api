using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public interface ICreditorBankAccountsClient
    {
        IPagerBuilder<GetCreditorBankAccountsRequest, CreditorBankAccount> BuildPager();
        Task<Response<CreditorBankAccount>> CreateAsync(CreateCreditorBankAccountRequest request);
        Task<Response<CreditorBankAccount>> DisableAsync(DisableCreditorBankAccountRequest request);
        Task<Response<CreditorBankAccount>> ForIdAsync(string creditorBankAccountId);
        Task<PagedResponse<CreditorBankAccount>> GetPageAsync();
        Task<PagedResponse<CreditorBankAccount>> GetPageAsync(GetCreditorBankAccountsRequest request);
    }
}