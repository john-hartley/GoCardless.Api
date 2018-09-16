using GoCardless.Api.Core;
using GoCardless.Api.Core.Paging;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public interface IRefundsClient
    {
        Task<PagedResponse<Refund>> AllAsync();
        Task<PagedResponse<Refund>> AllAsync(AllRefundsRequest request);
        Task<Response<Refund>> CreateAsync(CreateRefundRequest request);
        Task<Response<Refund>> ForIdAsync(string refundId);
        Task<Response<Refund>> UpdateAsync(UpdateRefundRequest request);
    }
}