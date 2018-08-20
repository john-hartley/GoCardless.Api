using Flurl;
using Flurl.Http;
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
    }
}