using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public interface IRefundsClient
    {
        Task<PagedResponse<Refund>> AllAsync();
        Task<PagedResponse<Refund>> AllAsync(AllRefundsRequest request);
        Task<RefundResponse> CreateAsync(CreateRefundRequest request);
        Task<RefundResponse> ForIdAsync(string refundId);
        Task<UpdateRefundResponse> UpdateAsync(UpdateRefundRequest request);
    }
}