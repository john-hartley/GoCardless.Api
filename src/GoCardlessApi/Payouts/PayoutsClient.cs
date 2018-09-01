using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Payouts
{
    public class PayoutsClient : ApiClientBase, IPayoutsClient
    {
        public PayoutsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllPayoutsResponse> AllAsync()
        {
            return GetAsync<AllPayoutsResponse>("payouts");
        }

        public Task<AllPayoutsResponse> AllAsync(AllPayoutsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllPayoutsResponse>("payouts", request.ToReadOnlyDictionary());
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