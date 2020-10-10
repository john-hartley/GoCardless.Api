using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public interface IMandateImportEntriesClient
    {
        Task<Response<MandateImportEntry>> AddAsync(AddMandateImportEntryOptions options);
        IPagerBuilder<GetMandateImportEntriesOptions, MandateImportEntry> BuildPager();
        Task<PagedResponse<MandateImportEntry>> GetPageAsync(GetMandateImportEntriesOptions options);
    }
}