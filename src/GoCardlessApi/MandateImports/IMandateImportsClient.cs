using System.Threading.Tasks;

namespace GoCardlessApi.MandateImports
{
    public interface IMandateImportsClient
    {
        Task<CancelMandateImportResponse> CancelAsync(string mandateImportId);
        Task<CreateMandateImportResponse> CreateAsync(CreateMandateImportRequest request);
        Task<MandateImportResponse> ForIdAsync(string mandateImportId);
        Task<SubmitMandateImportResponse> SubmitAsync(string mandateImportId);
    }
}