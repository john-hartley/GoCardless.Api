﻿using Flurl.Http;
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
            _apiClient = apiClient;
        }

        public async Task<Response<MandatePdf>> CreateAsync(CreateMandatePdfRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<MandatePdf>>(
                "mandate_pdfs",
                new { mandate_pdfs = options },
                request =>
                {
                    request.AppendPathSegment("mandate_pdfs");
                    if (!string.IsNullOrWhiteSpace(options.Language))
                    {
                        request.WithHeader("Accept-Language", options.Language);
                    }
                });
        }
    }
}