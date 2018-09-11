using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public interface IMandateImportEntriesClient
    {
        Task<AddMandateImportEntryResponse> AddAsync(AddMandateImportEntryRequest request);
        Task<AllMandateImportEntriesResponse> AllAsync(AllMandateImportEntriesRequest request);
    }
}