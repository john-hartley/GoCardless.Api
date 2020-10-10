using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public interface IPayoutItemsClient
    {
        IPagerBuilder<GetPayoutItemsOptions, PayoutItem> BuildPager();
        Task<PagedResponse<PayoutItem>> GetPageAsync(GetPayoutItemsOptions options);
    }
}