using System.Threading.Tasks;

namespace GoCardless.Api.MandatePdfs
{
    public interface IMandatePdfsClient
    {
        Task<CreateMandatePdfResponse> CreateAsync(CreateMandatePdfRequest request);
    }
}