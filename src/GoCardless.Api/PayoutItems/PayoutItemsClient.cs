using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public class PayoutItemsClient : IPayoutItemsClient
    {
        private readonly IApiClient _apiClient;

        public PayoutItemsClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetPayoutItemsOptions, PayoutItem> BuildPager()
        {
            return new Pager<GetPayoutItemsOptions, PayoutItem>(GetPageAsync);
        }

        public async Task<PagedResponse<PayoutItem>> GetPageAsync(GetPayoutItemsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Payout))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Payout));
            }

            return await _apiClient.GetAsync<PagedResponse<PayoutItem>>(request =>
            {
                request
                    .AppendPathSegment("payout_items")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }
    }
}