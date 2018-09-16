using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Payouts
{
    public class PayoutsClient : ApiClientBase, IPayoutsClient
    {
        public PayoutsClient(ClientConfiguration configuration) : base(configuration) { }

        public IPagerBuilder<GetPayoutsRequest, Payout> BuildPager()
        {
            return new Pager<GetPayoutsRequest, Payout>(GetPageAsync);
        }

        public Task<Response<Payout>> ForIdAsync(string payoutId)
        {
            if (string.IsNullOrWhiteSpace(payoutId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(payoutId));
            }

            return GetAsync<Response<Payout>>($"payouts/{payoutId}");
        }

        public Task<PagedResponse<Payout>> GetPageAsync()
        {
            return GetAsync<PagedResponse<Payout>>("payouts");
        }

        public Task<PagedResponse<Payout>> GetPageAsync(GetPayoutsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Payout>>("payouts", request.ToReadOnlyDictionary());
        }
    }
}