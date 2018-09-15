using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImports
{
    public interface IMandateImportsClient
    {
        Task<Response<MandateImport>> CancelAsync(string mandateImportId);
        Task<Response<MandateImport>> CreateAsync(CreateMandateImportRequest request);
        Task<Response<MandateImport>> ForIdAsync(string mandateImportId);
        Task<Response<MandateImport>> SubmitAsync(string mandateImportId);
    }
}