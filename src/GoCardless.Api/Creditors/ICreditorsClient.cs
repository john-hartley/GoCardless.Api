using GoCardless.Api.Core;
using GoCardless.Api.Core.Paging;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public interface ICreditorsClient
    {
        Task<PagedResponse<Creditor>> AllAsync();
        Task<PagedResponse<Creditor>> AllAsync(AllCreditorsRequest request);
        Task<Response<Creditor>> ForIdAsync(string creditorId);
        Task<Response<Creditor>> UpdateAsync(UpdateCreditorRequest request);
    }
}