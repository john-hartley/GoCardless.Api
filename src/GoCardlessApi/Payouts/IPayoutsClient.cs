using System.Threading.Tasks;

namespace GoCardlessApi.Payouts
{
    public interface IPayoutsClient
    {
        Task<AllPayoutsResponse> AllAsync();
        Task<AllPayoutsResponse> AllAsync(AllPayoutsRequest request);
        Task<PayoutResponse> ForIdAsync(string payoutId);
    }
}