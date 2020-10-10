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
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetMandatesRequest, Mandate> BuildPager()
        {
            return new Pager<GetMandatesRequest, Mandate>(GetPageAsync);
        }

        public async Task<Response<Mandate>> CancelAsync(CancelMandateRequest options)
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
                "mandates",
                request =>
                {
                    request.AppendPathSegment($"mandates/{options.Id}/actions/cancel");
                },
                new { mandates = options });
        }

        public async Task<Response<Mandate>> CreateAsync(CreateMandateRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Mandate>>(
                "mandates",
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

        public async Task<Response<Mandate>> ReinstateAsync(ReinstateMandateRequest options)
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
                "mandates",
                request =>
                {
                    request.AppendPathSegment($"mandates/{options.Id}/actions/reinstate");
                },
                new { mandates = options });
        }

        public async Task<Response<Mandate>> UpdateAsync(UpdateMandateRequest options)
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
                new { mandates = options },
                request =>
                {
                    request.AppendPathSegment($"mandates/{options.Id}");
                });
        }
    }
}