using GoCardless.Api.Core.Paging;
using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public interface IPayoutItemsClient
    {
        Task<PagedResponse<PayoutItem>> ForPayoutAsync(PayoutItemsRequest request);
    }
}