using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.BankDetailsLookups
{
    public class BankDetailsLookupsClient : IBankDetailsLookupsClient
    {
        private readonly IApiClient _apiClient;

        public BankDetailsLookupsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public BankDetailsLookupsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<BankDetailsLookup>> LookupAsync(BankDetailsLookupOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<BankDetailsLookup>>(
                request =>
                {
                    request.AppendPathSegment("bank_details_lookups");
                },
                new { bank_details_lookups = options });
        }
    }
}