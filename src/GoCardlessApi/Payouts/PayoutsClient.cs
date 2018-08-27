using GoCardlessApi.Core;
using System.Threading.Tasks;

namespace GoCardlessApi.Payouts
{
    public class PayoutsClient : ApiClientBase
    {
        public PayoutsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllPayoutsResponse> AllAsync()
        {
            return GetAsync<AllPayoutsResponse>("payouts");
        }
    }
}