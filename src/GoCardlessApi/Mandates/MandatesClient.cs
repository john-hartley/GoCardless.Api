using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Mandates
{
    public class MandatesClient : ApiClientBase, IMandatesClient
    {
        public MandatesClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllMandatesResponse> AllAsync()
        {
            return GetAsync<AllMandatesResponse>("mandates");
        }

        public Task<AllMandatesResponse> AllAsync(AllMandatesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllMandatesResponse>("mandates", request.ToReadOnlyDictionary());
        }

        public Task<CancelMandateResponse> CancelAsync(CancelMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<CancelMandateResponse>(
                $"mandates/{request.Id}/actions/cancel",
                new { mandates = request }
            );
        }

        public Task<CreateMandateResponse> CreateAsync(CreateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateMandateResponse>(
                "mandates",
                new { mandates = request },
                idempotencyKey
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

        public Task<ReinstateMandateResponse> ReinstateAsync(ReinstateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<ReinstateMandateResponse>(
                $"mandates/{request.Id}/actions/reinstate",
                new { mandates = request }
            );
        }

        public Task<UpdateMandateResponse> UpdateAsync(UpdateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<UpdateMandateResponse>(
                $"mandates/{request.Id}",
                new { mandates = request }
            );
        }
    }
}