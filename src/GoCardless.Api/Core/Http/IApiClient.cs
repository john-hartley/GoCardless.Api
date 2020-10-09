using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public interface IApiClient
    {
        ClientConfiguration Configuration { get; }

        Task<TResponse> GetAsync<TResponse>(Action<IFlurlRequest> action);
        Task<TResponse> PostAsync<TResponse>(string relativeEndpoint, object envelope, Action<IFlurlRequest> action);
        Task<TResponse> PutAsync<TResponse>(string relativeEndpoint, object envelope);
    }
}