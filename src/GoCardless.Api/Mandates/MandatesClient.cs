using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public class MandatesClient : ApiClient, IMandatesClient
    {
        private readonly IApiClient _apiClient;

        public MandatesClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

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

        public async Task<Response<Mandate>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Mandate>>(request =>
            {
                request.AppendPathSegment($"mandates/{id}");
            });
        }

        public async Task<PagedResponse<Mandate>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Mandate>>(request =>
            {
                request.AppendPathSegment("mandates");
            });
        }

        public async Task<PagedResponse<Mandate>> GetPageAsync(GetMandatesRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Mandate>>(request =>
            {
                request
                    .AppendPathSegment("mandates")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
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