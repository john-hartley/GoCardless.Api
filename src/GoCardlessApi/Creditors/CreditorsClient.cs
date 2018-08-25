using GoCardlessApi.Core;
using System.Threading.Tasks;

namespace GoCardlessApi.Creditors
{
    public class CreditorsClient : ApiClientBase, ICreditorsClient
    {
        public CreditorsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllCreditorResponse> AllAsync()
        {
            return GetAsync<AllCreditorResponse>("creditors");
        }

        public Task<CreditorResponse> ForIdAsync(string creditorId)
        {
            return GetAsync<CreditorResponse>("creditors", creditorId);
        }

        public Task<UpdateCreditorResponse> UpdateAsync(UpdateCreditorRequest request)
        {
            return PutAsync<UpdateCreditorRequest, UpdateCreditorResponse>(
                new { creditors = request },
                "creditors",
                request.Id
            );
        }
    }
}