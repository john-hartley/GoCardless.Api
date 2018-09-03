using System.Threading.Tasks;

namespace GoCardlessApi.MandatePdfs
{
    public interface IMandatePdfsClient
    {
        Task<CreateMandatePdfResponse> CreateAsync(CreateMandatePdfRequest request);
    }
}