using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Payouts
{
    public class PayoutsClient : IPayoutsClient
    {
        private readonly ApiClient _apiClient;

        public PayoutsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<Payout>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"payouts/{id}")
                    .GetJsonAsync<Response<Payout>>();
            });
        }

        public async Task<PagedResponse<Payout>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("payouts")
                    .GetJsonAsync<PagedResponse<Payout>>();
            });
        }

        public async Task<PagedResponse<Payout>> GetPageAsync(GetPayoutsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("payouts")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Payout>>();
            });
        }

        public IPager<GetPayoutsOptions, Payout> PageUsing(GetPayoutsOptions options)
        {
            return new Pager<GetPayoutsOptions, Payout>(GetPageAsync, options);
        }
    }
}