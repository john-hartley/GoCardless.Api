using System.Threading.Tasks;

namespace GoCardlessApi.PayoutItems
{
    public interface IPayoutItemsClient
    {
        Task<PayoutItemsResponse> ForPayoutAsync(PayoutItemsRequest request);
    }
}