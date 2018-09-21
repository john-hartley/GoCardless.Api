using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public interface IMandatesClient
    {
        IPagerBuilder<GetMandatesRequest, Mandate> BuildPager();
        Task<Response<Mandate>> CancelAsync(CancelMandateRequest request);
        Task<Response<Mandate>> CreateAsync(CreateMandateRequest request);
        Task<Response<Mandate>> ForIdAsync(string id);
        Task<PagedResponse<Mandate>> GetPageAsync();
        Task<PagedResponse<Mandate>> GetPageAsync(GetMandatesRequest request);
        Task<Response<Mandate>> ReinstateAsync(ReinstateMandateRequest request);
        Task<Response<Mandate>> UpdateAsync(UpdateMandateRequest request);
    }
}