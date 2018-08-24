﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

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

        internal async Task<TResponse> GetAsync<TResponse>(params string[] pathSegments)
        {
            return await _baseRequest
                .AppendPathSegments(pathSegments)
                .GetJsonAsync<TResponse>()
                .ConfigureAwait(false);
        }

        internal async Task<TResponse> PutAsync<TRequest, TResponse>(object envelope, params string[] pathSegments)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            try
            {
                var response = await _configuration.BaseUri
                    .WithHeaders(_configuration.Headers)
                    .AppendPathSegments(pathSegments)
                    .PutJsonAsync(envelope)
                    .ReceiveJson<TResponse>();
                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
            }

            return default(TResponse);
        }
    }
}
