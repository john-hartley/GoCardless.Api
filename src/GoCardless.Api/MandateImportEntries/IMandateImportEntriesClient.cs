using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public interface IMandateImportEntriesClient
    {
        Task<Response<MandateImportEntry>> AddAsync(AddMandateImportEntryRequest request);
        IPagerBuilder<GetMandateImportEntriesRequest, MandateImportEntry> BuildPager();
        Task<PagedResponse<MandateImportEntry>> GetPageAsync(GetMandateImportEntriesRequest request);
    }
}