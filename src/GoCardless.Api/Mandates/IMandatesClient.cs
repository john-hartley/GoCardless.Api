using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public interface IMandatesClient
    {
        Task<PagedResponse<Mandate>> AllAsync();
        Task<PagedResponse<Mandate>> AllAsync(AllMandatesRequest request);
        Task<MandateResponse> CancelAsync(CancelMandateRequest request);
        Task<MandateResponse> CreateAsync(CreateMandateRequest request);
        Task<MandateResponse> ForIdAsync(string mandateId);
        Task<MandateResponse> ReinstateAsync(ReinstateMandateRequest request);
        Task<MandateResponse> UpdateAsync(UpdateMandateRequest request);
    }
}