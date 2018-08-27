using GoCardlessApi.Core;
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

        public Task<PayoutResponse> ForIdAsync(string payoutId)
        {
            return GetAsync<PayoutResponse>("payouts", payoutId);
        }
    }
}