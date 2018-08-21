using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class CreditorsClient : ICreditorsClient
    {
        private readonly ClientConfiguration _configuration;

        public CreditorsClient(ClientConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CreditorResponse> ForIdAsync(string creditorId)
        {
            return await _configuration.BaseUri
                .WithHeader("Authorization", $"Bearer {_configuration.AccessToken}")
                .WithHeader("GoCardless-Version", "2015-07-06")
                .AppendPathSegments("creditors", creditorId)
                .GetJsonAsync<CreditorResponse>()
                .ConfigureAwait(false);
        }

        public async Task<UpdateCreditorResponse> UpdateAsync(UpdateCreditorRequest request)
        {
            var envelope = new { creditors = request };
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            try
            {
                var response = await _configuration.BaseUri
                    .WithHeader("Authorization", $"Bearer {_configuration.AccessToken}")
                    .WithHeader("GoCardless-Version", "2015-07-06")
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