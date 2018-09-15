using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Payouts
{
    public class PayoutsClient : ApiClientBase, IPayoutsClient
    {
        public PayoutsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<Payout>> AllAsync()
        {
            return GetAsync<PagedResponse<Payout>>("payouts");
        }

        public Task<PagedResponse<Payout>> AllAsync(AllPayoutsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Payout>>("payouts", request.ToReadOnlyDictionary());
        }

        public Task<PayoutResponse> ForIdAsync(string payoutId)
        {
            if (string.IsNullOrWhiteSpace(payoutId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(payoutId));
            }

            return GetAsync<PayoutResponse>($"payouts/{payoutId}");
        }
    }
}