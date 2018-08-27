using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Mandates
{
    public class MandatesClient : ApiClientBase, IMandatesClient
    {
        public MandatesClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateMandateResponse> CreateAsync(CreateMandateRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateMandateRequest, CreateMandateResponse>(
                "mandates",
                new { mandates = request },
                idempotencyKey
            );
        }

        public Task<AllMandatesResponse> AllAsync()
        {
            return GetAsync<AllMandatesResponse>("mandates");
        }

        public Task<MandateResponse> ForIdAsync(string mandateId)
        {
            return GetAsync<MandateResponse>($"mandates/{mandateId}");
        }

        public Task<UpdateMandateResponse> UpdateAsync(UpdateMandateRequest request)
        {
            return PutAsync<UpdateMandateRequest, UpdateMandateResponse>(
                $"mandates/{request.Id}",
                new { mandates = request }
            );
        }

        public Task<CancelMandateResponse> CancelAsync(CancelMandateRequest request)
        {
            return PostAsync<CancelMandateRequest, CancelMandateResponse>(
                $"mandates/{request.Id}/actions/cancel",
                new { mandates = request }
            );
        }

        public Task<ReinstateMandateResponse> ReinstateAsync(ReinstateMandateRequest request)
        {
            return PostAsync<ReinstateMandateRequest, ReinstateMandateResponse>(
                $"mandates/{request.Id}/actions/reinstate",
                new { mandates = request }
            );
        }
    }
}