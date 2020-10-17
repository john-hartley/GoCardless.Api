using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandatePdfs
{
    public class MandatePdfsClient : IMandatePdfsClient
    {
        private readonly IApiClient _apiClient;

        public MandatePdfsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public MandatePdfsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<MandatePdf>> CreateAsync(CreateMandatePdfOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentAsync<Response<MandatePdf>>(
                request =>
                {
                    request.AppendPathSegment("mandate_pdfs");
                    if (!string.IsNullOrWhiteSpace(options.Language))
                    {
                        request.WithHeader("Accept-Language", options.Language);
                    }
                },
                new { mandate_pdfs = options });
        }
    }
}