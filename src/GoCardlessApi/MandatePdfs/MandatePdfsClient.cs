using GoCardlessApi.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardlessApi.MandatePdfs
{
    public class MandatePdfsClient : ApiClientBase, IMandatePdfsClient
    {
        public MandatePdfsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateMandatePdfResponse> CreateAsync(CreateMandatePdfRequest request)
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

            return PostAsync<CreateMandatePdfResponse>(
                "mandate_pdfs",
                new { mandate_pdfs = request },
                customHeaders
            );
        }
    }
}