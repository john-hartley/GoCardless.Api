using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.BankDetailsLookups
{
    public class BankDetailsLookupsClient : IBankDetailsLookupsClient
    {
        private readonly ApiClient _apiClient;

        public BankDetailsLookupsClient(ApiClientConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<BankDetailsLookup>> LookupAsync(BankDetailsLookupOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("bank_details_lookups")
                    .PostJsonAsync(new { bank_details_lookups = options })
                    .ReceiveJson<Response<BankDetailsLookup>>();
            });
        }
    }
}