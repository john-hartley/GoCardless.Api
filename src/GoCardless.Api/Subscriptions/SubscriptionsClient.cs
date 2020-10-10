using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public class SubscriptionsClient : ISubscriptionsClient
    {
        private readonly IApiClient _apiClient;

        public SubscriptionsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public SubscriptionsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public IPagerBuilder<GetSubscriptionsOptions, Subscription> BuildPager()
        {
            return new Pager<GetSubscriptionsOptions, Subscription>(GetPageAsync);
        }

        public async Task<Response<Subscription>> CancelAsync(CancelSubscriptionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PostAsync<Response<Subscription>>(
                request =>
                {
                    request.AppendPathSegment($"subscriptions/{options.Id}/actions/cancel");
                },
                new { subscriptions = options });
        }

        public async Task<Response<Subscription>> CreateAsync(CreateSubscriptionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Subscription>>(
                request =>
                {
                    request
                        .AppendPathSegment("subscriptions")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                },
                new { subscriptions = options });
        }

        public async Task<Response<Subscription>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Subscription>>(request =>
            {
                request.AppendPathSegment($"subscriptions/{id}");
            });
        }

        public async Task<PagedResponse<Subscription>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Subscription>>(request =>
            {
                request.AppendPathSegment("subscriptions");
            });
        }

        public async Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Subscription>>(request =>
            {
                request
                    .AppendPathSegment("subscriptions")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public async Task<Response<Subscription>> UpdateAsync(UpdateSubscriptionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PutAsync<Response<Subscription>>(
                request =>
                {
                    request.AppendPathSegment($"subscriptions/{options.Id}");
                },
                new { subscriptions = options });
        }
    }
}