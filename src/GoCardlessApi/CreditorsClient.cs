using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class CreditorsClient
    {
        private readonly string _accessToken;

        public CreditorsClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<CreditorResponse> ForIdAsync(string creditorId)
        {
            return await "https://api-sandbox.gocardless.com/"
                .WithHeader("Authorization", $"Bearer {_accessToken}")
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
                var response = await "https://api-sandbox.gocardless.com/"
                    .WithHeader("Authorization", $"Bearer {_accessToken}")
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