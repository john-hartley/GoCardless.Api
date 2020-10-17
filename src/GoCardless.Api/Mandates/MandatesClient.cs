using Flurl.Http;
using GoCardless.Api.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Mandates
{
    public class MandatesClient : IMandatesClient
    {
        private readonly ApiClient _apiClient;

        public MandatesClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"mandates/{options.Id}/actions/cancel")
                    .PostJsonAsync(new { mandates = options })
                    .ReceiveJson<Response<Mandate>>();
            });
        }

        public async Task<Response<Mandate>> CreateAsync(CreateMandateOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                request =>
                {
                    return request
                        .AppendPathSegment("mandates")
                        .PostJsonAsync(new { mandates = options })
                        .ReceiveJson<Response<Mandate>>();
                });
        }

        public async Task<Response<Mandate>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"mandates/{id}")
                    .GetJsonAsync<Response<Mandate>>();
            });
        }

        public async Task<PagedResponse<Mandate>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("mandates")
                    .GetJsonAsync<PagedResponse<Mandate>>();
            });
        }

        public async Task<PagedResponse<Mandate>> GetPageAsync(GetMandatesOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("mandates")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Mandate>>();
            });
        }

        public IPager<GetMandatesOptions, Mandate> PageFrom(GetMandatesOptions options)
        {
            return new Pager<GetMandatesOptions, Mandate>(GetPageAsync, options);
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"mandates/{options.Id}/actions/reinstate")
                    .PostJsonAsync(new { mandates = options })
                    .ReceiveJson<Response<Mandate>>();
            });
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"mandates/{options.Id}")
                    .PutJsonAsync(new { mandates = options })
                    .ReceiveJson<Response<Mandate>>();
            });
        }
    }
}