using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Paging;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public class PayoutItemsClient : ApiClientBase, IPayoutItemsClient
    {
        public PayoutItemsClient(ClientConfiguration configuration) : base(configuration) { }

        public IPagerBuilder<GetPayoutItemsRequest, PayoutItem> BuildPager()
        {
            return new Pager<GetPayoutItemsRequest, PayoutItem>(GetPageAsync);
        }

        public Task<PagedResponse<PayoutItem>> GetPageAsync(GetPayoutItemsRequest request)
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