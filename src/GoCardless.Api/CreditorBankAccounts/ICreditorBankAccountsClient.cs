using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.CreditorBankAccounts
{
    public interface ICreditorBankAccountsClient : IPageable<GetCreditorBankAccountsOptions, CreditorBankAccount>
    {
        Task<Response<CreditorBankAccount>> CreateAsync(CreateCreditorBankAccountOptions options);
        Task<Response<CreditorBankAccount>> DisableAsync(DisableCreditorBankAccountOptions options);
        Task<Response<CreditorBankAccount>> ForIdAsync(string id);
        Task<PagedResponse<CreditorBankAccount>> GetPageAsync();
        Task<PagedResponse<CreditorBankAccount>> GetPageAsync(GetCreditorBankAccountsOptions options);
    }
}