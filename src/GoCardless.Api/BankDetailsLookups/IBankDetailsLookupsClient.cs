using GoCardless.Api.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.BankDetailsLookups
{
    public interface IBankDetailsLookupsClient
    {
        Task<Response<BankDetailsLookup>> LookupAsync(BankDetailsLookupOptions options);
    }
}