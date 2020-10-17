using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.PayoutItems
{
    public interface IPayoutItemsClient : IPageable<GetPayoutItemsOptions, PayoutItem>
    {
        Task<PagedResponse<PayoutItem>> GetPageAsync(GetPayoutItemsOptions options);
    }
}