using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public interface IMandateImportEntriesClient : IPageable<GetMandateImportEntriesOptions, MandateImportEntry>
    {
        Task<Response<MandateImportEntry>> AddAsync(AddMandateImportEntryOptions options);
        Task<PagedResponse<MandateImportEntry>> GetPageAsync(GetMandateImportEntriesOptions options);
    }
}