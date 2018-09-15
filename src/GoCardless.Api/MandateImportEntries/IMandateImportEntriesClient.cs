using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public interface IMandateImportEntriesClient
    {
        Task<MandateImportEntryResponse> AddAsync(AddMandateImportEntryRequest request);
        Task<PagedResponse<MandateImportEntry>> AllAsync(AllMandateImportEntriesRequest request);
    }
}