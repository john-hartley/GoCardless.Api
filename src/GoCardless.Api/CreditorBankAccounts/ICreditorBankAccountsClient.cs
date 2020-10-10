using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public interface ICreditorBankAccountsClient
    {
        IPagerBuilder<GetCreditorBankAccountsOptions, CreditorBankAccount> BuildPager();
        Task<Response<CreditorBankAccount>> CreateAsync(CreateCreditorBankAccountOptions request);
        Task<Response<CreditorBankAccount>> DisableAsync(DisableCreditorBankAccountOptions request);
        Task<Response<CreditorBankAccount>> ForIdAsync(string id);
        Task<PagedResponse<CreditorBankAccount>> GetPageAsync();
        Task<PagedResponse<CreditorBankAccount>> GetPageAsync(GetCreditorBankAccountsOptions request);
    }
}