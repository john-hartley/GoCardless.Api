using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImports
{
    public interface IMandateImportsClient
    {
        Task<Response<MandateImport>> CancelAsync(string id);
        Task<Response<MandateImport>> CreateAsync(CreateMandateImportRequest request);
        Task<Response<MandateImport>> ForIdAsync(string id);
        Task<Response<MandateImport>> SubmitAsync(string id);
    }
}