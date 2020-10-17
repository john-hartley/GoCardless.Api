using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImports
{
    public class MandateImportsClient : IMandateImportsClient
    {
        private readonly IApiClient _apiClient;

        public MandateImportsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public MandateImportsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<MandateImport>> CancelAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.IdempotentAsync<Response<MandateImport>>(
                request =>
                {
                    request.AppendPathSegment($"mandate_imports/{id}/actions/cancel");
                });
        }

        public async Task<Response<MandateImport>> CreateAsync(CreateMandateImportOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentAsync<Response<MandateImport>>(
                request =>
                {
                    request.AppendPathSegment("mandate_imports");
                },
                new { mandate_imports = options });
        }

        public async Task<Response<MandateImport>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync<Response<MandateImport>>(request =>
            {
                request.AppendPathSegment($"mandate_imports/{id}");
            });
        }

        public async Task<Response<MandateImport>> SubmitAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.IdempotentAsync<Response<MandateImport>>(
                request =>
                {
                    request.AppendPathSegment($"mandate_imports/{id}/actions/submit");
                });
        }
    }
}