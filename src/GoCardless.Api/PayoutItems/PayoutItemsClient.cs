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
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public PayoutItemsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
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

            return await _apiClient.RequestAsync<PagedResponse<PayoutItem>>(request =>
            {
                request
                    .AppendPathSegment("payout_items")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public IPager<GetPayoutItemsOptions, PayoutItem> PageFrom(GetPayoutItemsOptions options)
        {
            return new Pager<GetPayoutItemsOptions, PayoutItem>(GetPageAsync, options);
        }
    }
}