using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public class PayoutItemsClient : ApiClientBase, IPayoutItemsClient
    {
        public PayoutItemsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<PayoutItem>> ForPayoutAsync(PayoutItemsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Payout))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Payout));
            }
            
            return GetAsync<PagedResponse<PayoutItem>>(
                "payout_items",
                request.ToReadOnlyDictionary()
            );
        }
    }
}