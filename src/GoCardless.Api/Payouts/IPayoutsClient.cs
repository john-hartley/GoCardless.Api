using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Payouts
{
    public interface IPayoutsClient
    {
        Task<PagedResponse<Payout>> AllAsync();
        Task<PagedResponse<Payout>> AllAsync(AllPayoutsRequest request);
        Task<PayoutResponse> ForIdAsync(string payoutId);
    }
}