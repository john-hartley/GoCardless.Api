using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Paging;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public class MandatesClient : ApiClientBase, IMandatesClient
    {
        public MandatesClient(ClientConfiguration configuration) : base(configuration) { }

        public IPagerBuilder<GetMandatesRequest, Mandate> BuildPager()
        {
            return new Pager<GetMandatesRequest, Mandate>(GetPageAsync);
        }

        public Task<Response<Mandate>> CancelAsync(CancelMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<Mandate>>(
                $"mandates/{request.Id}/actions/cancel",
                new { mandates = request }
            );
        }

        public Task<Response<Mandate>> CreateAsync(CreateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<Mandate>>(
                "mandates",
                new { mandates = request },
                request.IdempotencyKey
            );
        }

        public Task<Response<Mandate>> ForIdAsync(string mandateId)
        {
            if (string.IsNullOrWhiteSpace(mandateId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(mandateId));
            }

            return GetAsync<Response<Mandate>>($"mandates/{mandateId}");
        }

        public Task<PagedResponse<Mandate>> GetPageAsync()
        {
            return GetAsync<PagedResponse<Mandate>>("mandates");
        }

        public Task<PagedResponse<Mandate>> GetPageAsync(GetMandatesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Mandate>>("mandates", request.ToReadOnlyDictionary());
        }

        public Task<Response<Mandate>> ReinstateAsync(ReinstateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<Mandate>>(
                $"mandates/{request.Id}/actions/reinstate",
                new { mandates = request }
            );
        }

        public Task<Response<Mandate>> UpdateAsync(UpdateMandateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<Mandate>>(
                $"mandates/{request.Id}",
                new { mandates = request }
            );
        }
    }
}