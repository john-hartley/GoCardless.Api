using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.MandatePdfs
{
    public interface IMandatePdfsClient
    {
        Task<Response<MandatePdf>> CreateAsync(CreateMandatePdfOptions options);
    }
}