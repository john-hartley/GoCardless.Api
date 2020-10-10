using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public class MandatesClient : IMandatesClient
    {
        private readonly IApiClient _apiClient;

        public MandatesClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public MandatesClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public IPagerBuilder<GetMandatesOptions, Mandate> BuildPager()
        {
            return new Pager<GetMandatesOptions, Mandate>(GetPageAsync);
        }

        public async Task<Response<Mandate>> CancelAsync(CancelMandateOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PostAsync<Response<Mandate>>(
                request =>
                {
                    request.AppendPathSegment($"mandates/{options.Id}/actions/cancel");
                },
                new { mandates = options });
        }

        public async Task<Response<Mandate>> CreateAsync(CreateMandateOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Mandate>>(
                request =>
                {
                    request
                        .AppendPathSegment("mandates")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                },
                new { mandates = options });
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

        public async Task<PagedResponse<Mandate>> GetPageAsync(GetMandatesOptions options)
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

        public async Task<Response<Mandate>> ReinstateAsync(ReinstateMandateOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PostAsync<Response<Mandate>>(
                request =>
                {
                    request.AppendPathSegment($"mandates/{options.Id}/actions/reinstate");
                },
                new { mandates = options });
        }

        public async Task<Response<Mandate>> UpdateAsync(UpdateMandateOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PutAsync<Response<Mandate>>(
                request =>
                {
                    request.AppendPathSegment($"mandates/{options.Id}");
                },
                new { mandates = options });
        }
    }
}