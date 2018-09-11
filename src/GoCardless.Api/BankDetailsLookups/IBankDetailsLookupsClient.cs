using System.Threading.Tasks;

namespace GoCardless.Api.BankDetailsLookups
{
    public interface IBankDetailsLookupsClient
    {
        Task<BankDetailsLookupResponse> LookupAsync(BankDetailsLookupRequest request);
    }
}