using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public interface IMandateImportEntriesClient
    {
        Task<AddMandateImportEntriesResponse> AddAsync(AddMandateImportEntriesRequest request);
        Task<AllMandateImportEntriesResponse> AllAsync(AllMandateImportEntriesRequest request);
    }
}