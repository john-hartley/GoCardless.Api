using System.Threading.Tasks;

namespace GoCardlessApi.BankDetailsLookups
{
    public interface IBankDetailsLookupsClient
    {
        Task<BankDetailsLookupResponse> LookupAsync(BankDetailsLookupRequest request);
    }
}