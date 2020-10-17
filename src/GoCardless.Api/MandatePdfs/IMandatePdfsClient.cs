using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.MandatePdfs
{
    public interface IMandatePdfsClient
    {
        Task<Response<MandatePdf>> CreateAsync(CreateMandatePdfOptions options);
    }
}