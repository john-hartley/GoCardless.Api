using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace GoCardlessApi
{
    public abstract class ApiClientBase
    {
        private readonly ClientConfiguration _configuration;
        private readonly IFlurlRequest _baseRequest;

        internal ApiClientBase(ClientConfiguration configuration)
        {
            _configuration = configuration;
            _baseRequest = _configuration.BaseUri.WithHeaders(_configuration.Headers);
        }

        internal async Task<TResponse> ForIdAsync<TResponse>(string endpoint, string resourceId)
        {
            return await _baseRequest
                .AppendPathSegments(endpoint, resourceId)
                .GetJsonAsync<TResponse>()
                .ConfigureAwait(false);
        }
    }
}
