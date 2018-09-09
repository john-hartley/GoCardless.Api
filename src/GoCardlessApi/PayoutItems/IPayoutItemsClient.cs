using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public interface IPayoutItemsClient
    {
        Task<PayoutItemsResponse> ForPayoutAsync(PayoutItemsRequest request);
    }
}