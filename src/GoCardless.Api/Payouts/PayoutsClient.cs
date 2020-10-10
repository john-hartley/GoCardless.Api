using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Payouts
{
    public class PayoutsClient : IPayoutsClient
    {
        private readonly IApiClient _apiClient;

        public PayoutsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public PayoutsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<Payout>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Payout>>(request =>
            {
                request.AppendPathSegment($"payouts/{id}");
            });
        }

        public async Task<PagedResponse<Payout>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Payout>>(request =>
            {
                request.AppendPathSegment("payouts");
            });
        }

        public async Task<PagedResponse<Payout>> GetPageAsync(GetPayoutsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Payout>>(request =>
            {
                request
                    .AppendPathSegment("payouts")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public IPager<GetPayoutsOptions, Payout> PageFrom(GetPayoutsOptions options)
        {
            return new Pager<GetPayoutsOptions, Payout>(GetPageAsync, options);
        }
    }
}