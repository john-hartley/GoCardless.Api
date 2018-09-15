using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public interface IMandateImportEntriesClient
    {
        Task<Response<MandateImportEntry>> AddAsync(AddMandateImportEntryRequest request);
        Task<PagedResponse<MandateImportEntry>> AllAsync(AllMandateImportEntriesRequest request);
    }
}