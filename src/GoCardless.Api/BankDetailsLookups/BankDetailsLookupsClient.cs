using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.BankDetailsLookups
{
    public class BankDetailsLookupsClient : ApiClientBase, IBankDetailsLookupsClient
    {
        public BankDetailsLookupsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<Response<BankDetailsLookup>> LookupAsync(BankDetailsLookupRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<BankDetailsLookup>>(
                "bank_details_lookups",
                new { bank_details_lookups = request }
            );
        }
    }
}