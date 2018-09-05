using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.BankDetailsLookups
{
    public class BankDetailsLookupsClient : ApiClientBase, IBankDetailsLookupsClient
    {
        public BankDetailsLookupsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<BankDetailsLookupResponse> LookupAsync(BankDetailsLookupRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<BankDetailsLookupResponse>(
                "bank_details_lookups",
                new { bank_details_lookups = request }
            );
        }
    }
}