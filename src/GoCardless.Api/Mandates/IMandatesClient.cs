using GoCardless.Api.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public interface IMandatesClient : IPageable<GetMandatesOptions, Mandate>
    {
        Task<Response<Mandate>> CancelAsync(CancelMandateOptions options);
        Task<Response<Mandate>> CreateAsync(CreateMandateOptions options);
        Task<Response<Mandate>> ForIdAsync(string id);
        Task<PagedResponse<Mandate>> GetPageAsync();
        Task<PagedResponse<Mandate>> GetPageAsync(GetMandatesOptions options);
        Task<Response<Mandate>> ReinstateAsync(ReinstateMandateOptions options);
        Task<Response<Mandate>> UpdateAsync(UpdateMandateOptions options);
    }
}