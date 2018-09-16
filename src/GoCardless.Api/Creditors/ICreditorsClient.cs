using GoCardless.Api.Core;
using GoCardless.Api.Core.Paging;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public interface ICreditorsClient
    {
        IPagerBuilder<GetCreditorsRequest, Creditor> BuildPager();
        Task<Response<Creditor>> ForIdAsync(string creditorId);
        Task<PagedResponse<Creditor>> GetPageAsync();
        Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsRequest request);
        Task<Response<Creditor>> UpdateAsync(UpdateCreditorRequest request);
    }
}