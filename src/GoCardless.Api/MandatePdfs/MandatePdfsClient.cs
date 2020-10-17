using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandatePdfs
{
    public class MandatePdfsClient : IMandatePdfsClient
    {
        private readonly ApiClient _apiClient;

        public MandatePdfsClient(ApiClientConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<MandatePdf>> CreateAsync(CreateMandatePdfOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                if (!string.IsNullOrWhiteSpace(options.Language))
                {
                    request.WithHeader("Accept-Language", options.Language);
                }

                return request
                    .AppendPathSegment("mandate_pdfs")
                    .PostJsonAsync(new { mandate_pdfs = options })
                    .ReceiveJson<Response<MandatePdf>>();
            });
        }
    }
}