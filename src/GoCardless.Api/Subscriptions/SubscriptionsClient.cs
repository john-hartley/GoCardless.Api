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
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetSubscriptionsRequest, Subscription> BuildPager()
        {
            return new Pager<GetSubscriptionsRequest, Subscription>(GetPageAsync);
        }

        public async Task<Response<Subscription>> CancelAsync(CancelSubscriptionRequest options)
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
                "subscriptions",
                request =>
                {
                    request.AppendPathSegment($"subscriptions/{options.Id}/actions/cancel");
                },
                new { subscriptions = options });
        }

        public async Task<Response<Subscription>> CreateAsync(CreateSubscriptionRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Subscription>>(
                "subscriptions",
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

        public async Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsRequest options)
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

        public async Task<Response<Subscription>> UpdateAsync(UpdateSubscriptionRequest options)
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
                new { subscriptions = options },
                request =>
                {
                    request.AppendPathSegment($"subscriptions/{options.Id}");
                });
        }
    }
}