using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public interface IMandatesClient
    {
        Task<AllMandatesResponse> AllAsync();
        Task<AllMandatesResponse> AllAsync(AllMandatesRequest request);
        Task<MandateResponse> CancelAsync(CancelMandateRequest request);
        Task<MandateResponse> CreateAsync(CreateMandateRequest request);
        Task<MandateResponse> ForIdAsync(string mandateId);
        Task<MandateResponse> ReinstateAsync(ReinstateMandateRequest request);
        Task<MandateResponse> UpdateAsync(UpdateMandateRequest request);
    }
}