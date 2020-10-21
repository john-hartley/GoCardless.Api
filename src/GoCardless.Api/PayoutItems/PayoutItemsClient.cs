using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.PayoutItems
{
    public class PayoutItemsClient : IPayoutItemsClient
    {
        private readonly ApiClient _apiClient;

        public PayoutItemsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("payout_items")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<PayoutItem>>();
            });
        }

        public IPager<GetPayoutItemsOptions, PayoutItem> PageUsing(GetPayoutItemsOptions options)
        {
            return new Pager<GetPayoutItemsOptions, PayoutItem>(GetPageAsync, options);
        }
    }
}