using System.Threading.Tasks;

namespace GoCardless.Api.MandateImports
{
    public interface IMandateImportsClient
    {
        Task<MandateImportResponse> CancelAsync(string mandateImportId);
        Task<MandateImportResponse> CreateAsync(CreateMandateImportRequest request);
        Task<MandateImportResponse> ForIdAsync(string mandateImportId);
        Task<MandateImportResponse> SubmitAsync(string mandateImportId);
    }
}