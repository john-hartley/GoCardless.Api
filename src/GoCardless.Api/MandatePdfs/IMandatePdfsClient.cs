using System.Threading.Tasks;

namespace GoCardless.Api.MandatePdfs
{
    public interface IMandatePdfsClient
    {
        Task<MandatePdfResponse> CreateAsync(CreateMandatePdfRequest request);
    }
}