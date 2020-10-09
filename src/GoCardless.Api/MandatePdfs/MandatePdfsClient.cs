using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardless.Api.MandatePdfs
{
    public class MandatePdfsClient : ApiClient, IMandatePdfsClient
    {
        public MandatePdfsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<Response<MandatePdf>> CreateAsync(CreateMandatePdfRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var customHeaders = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(request.Language))
            {
                customHeaders.Add("Accept-Language", request.Language);
            }

            return PostAsync<Response<MandatePdf>>(
                "mandate_pdfs",
                new { mandate_pdfs = request },
                customHeaders
            );
        }
    }
}