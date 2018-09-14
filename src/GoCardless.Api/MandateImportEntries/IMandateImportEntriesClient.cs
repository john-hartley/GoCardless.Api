using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public interface IMandateImportEntriesClient
    {
        Task<MandateImportEntryResponse> AddAsync(AddMandateImportEntryRequest request);
        Task<AllMandateImportEntriesResponse> AllAsync(AllMandateImportEntriesRequest request);
    }
}