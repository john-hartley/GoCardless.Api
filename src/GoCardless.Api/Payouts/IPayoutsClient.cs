using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Payouts
{
    public interface IPayoutsClient
    {
        IPagerBuilder<GetPayoutsRequest, Payout> BuildPager();
        Task<Response<Payout>> ForIdAsync(string payoutId);
        Task<PagedResponse<Payout>> GetPageAsync();
        Task<PagedResponse<Payout>> GetPageAsync(GetPayoutsRequest request);
    }
}