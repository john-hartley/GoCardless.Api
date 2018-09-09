using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public interface IMandatesClient
    {
        Task<AllMandatesResponse> AllAsync();
        Task<AllMandatesResponse> AllAsync(AllMandatesRequest request);
        Task<CancelMandateResponse> CancelAsync(CancelMandateRequest request);
        Task<CreateMandateResponse> CreateAsync(CreateMandateRequest request);
        Task<MandateResponse> ForIdAsync(string mandateId);
        Task<ReinstateMandateResponse> ReinstateAsync(ReinstateMandateRequest request);
        Task<UpdateMandateResponse> UpdateAsync(UpdateMandateRequest request);
    }
}