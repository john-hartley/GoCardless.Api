using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.MandatePdfs
{
    public class MandatePdfsClient : IMandatePdfsClient
    {
        private readonly ApiClient _apiClient;

        public MandatePdfsClient(GoCardlessConfiguration configuration)
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

            return await _apiClient.RequestAsync(async request =>
            {
                if (!string.IsNullOrWhiteSpace(options.Language))
                {
                    request.WithHeader("Accept-Language", options.Language);
                }

                return await request
                    .AppendPathSegment("mandate_pdfs")
                    .PostJsonAsync(new { mandate_pdfs = options })
                    .ReceiveJson<Response<MandatePdf>>();
            });
        }
    }
}