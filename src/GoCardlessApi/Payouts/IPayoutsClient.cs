using System.Threading.Tasks;

namespace GoCardlessApi.Payouts
{
    public interface IPayoutsClient
    {
        Task<AllPayoutsResponse> AllAsync();
        Task<PayoutResponse> ForIdAsync(string payoutId);
    }
}