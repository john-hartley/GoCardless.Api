using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.MandateImports
{
    public interface IMandateImportsClient
    {
        Task<Response<MandateImport>> CancelAsync(string id);
        Task<Response<MandateImport>> CreateAsync(CreateMandateImportOptions options);
        Task<Response<MandateImport>> ForIdAsync(string id);
        Task<Response<MandateImport>> SubmitAsync(string id);
    }
}