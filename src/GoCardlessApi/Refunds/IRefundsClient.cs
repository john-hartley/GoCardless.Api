using System.Threading.Tasks;

namespace GoCardlessApi.Refunds
{
    public interface IRefundsClient
    {
        Task<AllRefundsResponse> AllAsync();
        Task<CreateRefundResponse> CreateAsync(CreateRefundRequest request);
        Task<RefundResponse> ForIdAsync(string refundId);
        Task<UpdateRefundResponse> UpdateAsync(UpdateRefundRequest request);
    }
}