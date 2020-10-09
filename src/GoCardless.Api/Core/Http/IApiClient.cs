using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public interface IApiClient
    {
        Task<TResponse> GetAsync<TResponse>(string relativeEndpoint, IReadOnlyDictionary<string, object> queryParams = null);
        Task<TResponse> GetAsync<TResponse>(Action<IFlurlRequest> action);
        Task<TResponse> PostAsync<TResponse>(string relativeEndpoint);
        Task<TResponse> PostAsync<TResponse>(string relativeEndpoint, object envelope, IReadOnlyDictionary<string, string> customHeaders = null);
        Task<TResponse> PostAsync<TResponse>(string relativeEndpoint, object envelope, string idempotencyKey, IReadOnlyDictionary<string, string> customHeaders = null);
        Task<TResponse> PutAsync<TResponse>(string relativeEndpoint, object envelope);
    }
}