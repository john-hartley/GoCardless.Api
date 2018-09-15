using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.BankDetailsLookups
{
    public interface IBankDetailsLookupsClient
    {
        Task<Response<BankDetailsLookup>> LookupAsync(BankDetailsLookupRequest request);
    }
}