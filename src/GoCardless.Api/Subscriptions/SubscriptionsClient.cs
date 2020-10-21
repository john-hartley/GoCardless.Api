using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Subscriptions
{
    public class SubscriptionsClient : ISubscriptionsClient
    {
        private readonly ApiClient _apiClient;

        public SubscriptionsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"subscriptions/{options.Id}/actions/cancel")
                    .PostJsonAsync(new { subscriptions = options })
                    .ReceiveJson<Response<Subscription>>();
            });
        }

        public async Task<Response<Subscription>> CreateAsync(CreateSubscriptionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                request =>
                {
                    return request
                        .AppendPathSegment("subscriptions")
                        .PostJsonAsync(new { subscriptions = options })
                        .ReceiveJson<Response<Subscription>>();
                });
        }

        public async Task<Response<Subscription>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"subscriptions/{id}")
                    .GetJsonAsync<Response<Subscription>>();
            });
        }

        public async Task<PagedResponse<Subscription>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("subscriptions")
                    .GetJsonAsync<PagedResponse<Subscription>>();
            });
        }

        public async Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("subscriptions")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Subscription>>();
            });
        }

        public IPager<GetSubscriptionsOptions, Subscription> PageUsing(GetSubscriptionsOptions options)
        {
            return new Pager<GetSubscriptionsOptions, Subscription>(GetPageAsync, options);
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"subscriptions/{options.Id}")
                    .PutJsonAsync(new { subscriptions = options })
                    .ReceiveJson<Response<Subscription>>();
            });
        }
    }
}