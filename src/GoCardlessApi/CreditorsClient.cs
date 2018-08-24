using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class CreditorsClient : ApiClientBase, ICreditorsClient
    {
        private readonly ClientConfiguration _configuration;

        public CreditorsClient(ClientConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

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