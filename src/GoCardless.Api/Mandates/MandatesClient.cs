using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public class MandatesClient : ApiClientBase, IMandatesClient
    {
        public MandatesClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<Mandate>> AllAsync()
        {
            return GetAsync<PagedResponse<Mandate>>("mandates");
        }

        public Task<PagedResponse<Mandate>> AllAsync(AllMandatesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Mandate>>("mandates", request.ToReadOnlyDictionary());
        }

        public Task<MandateResponse> CancelAsync(CancelMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<MandateResponse>(
                $"mandates/{request.Id}/actions/cancel",
                new { mandates = request }
            );
        }

        public Task<MandateResponse> CreateAsync(CreateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<MandateResponse>(
                "mandates",
                new { mandates = request },
                request.IdempotencyKey
            );
        }

        public Task<MandateResponse> ForIdAsync(string mandateId)
        {
            if (string.IsNullOrWhiteSpace(mandateId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(mandateId));
            }

            return GetAsync<MandateResponse>($"mandates/{mandateId}");
        }

        public Task<MandateResponse> ReinstateAsync(ReinstateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<MandateResponse>(
                $"mandates/{request.Id}/actions/reinstate",
                new { mandates = request }
            );
        }

        public Task<MandateResponse> UpdateAsync(UpdateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<MandateResponse>(
                $"mandates/{request.Id}",
                new { mandates = request }
            );
        }
    }
}