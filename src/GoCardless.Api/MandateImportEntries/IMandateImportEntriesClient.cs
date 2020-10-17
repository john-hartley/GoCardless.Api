using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.MandateImportEntries
{
    public interface IMandateImportEntriesClient : IPageable<GetMandateImportEntriesOptions, MandateImportEntry>
    {
        Task<Response<MandateImportEntry>> AddAsync(AddMandateImportEntryOptions options);
        Task<PagedResponse<MandateImportEntry>> GetPageAsync(GetMandateImportEntriesOptions options);
    }
}