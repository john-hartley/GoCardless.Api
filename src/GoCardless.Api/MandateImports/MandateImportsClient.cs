using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.MandateImports
{
    public class MandateImportsClient : IMandateImportsClient
    {
        private readonly ApiClient _apiClient;

        public MandateImportsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<MandateImport>> CancelAsync(CancelMandateImportOptions options)
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
                    .AppendPathSegment($"mandate_imports/{options.Id}/actions/cancel")
                    .PostJsonAsync(new { })
                    .ReceiveJson<Response<MandateImport>>();
            });
        }

        public async Task<Response<MandateImport>> CreateAsync(CreateMandateImportOptions options)
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
                        .AppendPathSegment("mandate_imports")
                        .PostJsonAsync(new { mandate_imports = options })
                        .ReceiveJson<Response<MandateImport>>();
                });
        }

        public async Task<Response<MandateImport>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"mandate_imports/{id}")
                    .GetJsonAsync<Response<MandateImport>>();
            });
        }

        public async Task<Response<MandateImport>> SubmitAsync(SubmitMandateImportOptions options)
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
                    .AppendPathSegment($"mandate_imports/{options.Id}/actions/submit")
                    .PostJsonAsync(new { })
                    .ReceiveJson<Response<MandateImport>>();
            });
        }
    }
}