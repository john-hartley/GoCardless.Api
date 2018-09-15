using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public interface IMandatesClient
    {
        Task<PagedResponse<Mandate>> AllAsync();
        Task<PagedResponse<Mandate>> AllAsync(AllMandatesRequest request);
        Task<Response<Mandate>> CancelAsync(CancelMandateRequest request);
        Task<Response<Mandate>> CreateAsync(CreateMandateRequest request);
        Task<Response<Mandate>> ForIdAsync(string mandateId);
        Task<Response<Mandate>> ReinstateAsync(ReinstateMandateRequest request);
        Task<Response<Mandate>> UpdateAsync(UpdateMandateRequest request);
    }
}