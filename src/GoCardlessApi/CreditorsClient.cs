using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
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

        public async Task<CreditorResponse> ForIdAsync(string creditorId)
        {
            return await ForIdAsync<CreditorResponse>("creditors", creditorId);
        }

        public async Task<UpdateCreditorResponse> UpdateAsync(UpdateCreditorRequest request)
        {
            var envelope = new { creditors = request };
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            try
            {
                var response = await _configuration.BaseUri
                    .WithHeaders(_configuration.Headers)
                    .AppendPathSegments("creditors", request.Id)
                    .PutJsonAsync(envelope)
                    .ReceiveJson<UpdateCreditorResponse>();
                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
            }

            return null;
        }
    }
}