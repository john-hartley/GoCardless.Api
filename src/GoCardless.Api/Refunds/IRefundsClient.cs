using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public interface IRefundsClient
    {
        IPagerBuilder<GetRefundsRequest, Refund> BuildPager();
        Task<Response<Refund>> CreateAsync(CreateRefundRequest request);
        Task<Response<Refund>> ForIdAsync(string refundId);
        Task<PagedResponse<Refund>> GetPageAsync();
        Task<PagedResponse<Refund>> GetPageAsync(GetRefundsRequest request);
        Task<Response<Refund>> UpdateAsync(UpdateRefundRequest request);
    }
}