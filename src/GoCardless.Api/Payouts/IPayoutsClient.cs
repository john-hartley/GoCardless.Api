using System.Threading.Tasks;

namespace GoCardless.Api.Payouts
{
    public interface IPayoutsClient
    {
        Task<AllPayoutsResponse> AllAsync();
        Task<AllPayoutsResponse> AllAsync(AllPayoutsRequest request);
        Task<PayoutResponse> ForIdAsync(string payoutId);
    }
}