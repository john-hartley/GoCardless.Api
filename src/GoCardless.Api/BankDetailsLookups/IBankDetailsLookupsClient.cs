using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.BankDetailsLookups
{
    public interface IBankDetailsLookupsClient
    {
        Task<Response<BankDetailsLookup>> LookupAsync(BankDetailsLookupOptions options);
    }
}