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
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetPayoutsRequest, Payout> BuildPager()
        {
            return new Pager<GetPayoutsRequest, Payout>(GetPageAsync);
        }

        public async Task<Response<Payout>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Payout>>($"payouts/{id}");
        }

        public async Task<PagedResponse<Payout>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Payout>>("payouts");
        }

        public async Task<PagedResponse<Payout>> GetPageAsync(GetPayoutsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await _apiClient.GetAsync<PagedResponse<Payout>>("payouts", request.ToReadOnlyDictionary());
        }
    }
}