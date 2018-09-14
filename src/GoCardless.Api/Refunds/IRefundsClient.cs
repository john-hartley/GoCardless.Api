using System.Threading.Tasks;

namespace GoCardless.Api.Refunds
{
    public interface IRefundsClient
    {
        Task<AllRefundsResponse> AllAsync();
        Task<AllRefundsResponse> AllAsync(AllRefundsRequest request);
        Task<RefundResponse> CreateAsync(CreateRefundRequest request);
        Task<RefundResponse> ForIdAsync(string refundId);
        Task<UpdateRefundResponse> UpdateAsync(UpdateRefundRequest request);
    }
}