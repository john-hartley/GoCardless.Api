using GoCardless.Api.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public interface ICreditorsClient : IPageable<GetCreditorsOptions, Creditor>
    {
        Task<Response<Creditor>> ForIdAsync(string id);
        Task<PagedResponse<Creditor>> GetPageAsync();
        Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsOptions options);
        Task<Response<Creditor>> UpdateAsync(UpdateCreditorOptions options);
    }
}