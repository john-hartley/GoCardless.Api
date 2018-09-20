using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Creditors
{
    public interface ICreditorsClient
    {
        IPagerBuilder<GetCreditorsRequest, Creditor> BuildPager();
        Task<Response<Creditor>> ForIdAsync(string id);
        Task<PagedResponse<Creditor>> GetPageAsync();
        Task<PagedResponse<Creditor>> GetPageAsync(GetCreditorsRequest request);
        Task<Response<Creditor>> UpdateAsync(UpdateCreditorRequest request);
    }
}