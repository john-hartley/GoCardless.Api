using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public interface IApiClient
    {
        Task<TResponse> GetAsync<TResponse>(
            Action<IFlurlRequest> configure);

        Task<TResponse> PostAsync<TResponse>(
            string relativeEndpoint, 
            Action<IFlurlRequest> configure, 
            object envelope = null);

        Task<TResponse> PutAsync<TResponse>(
            Action<IFlurlRequest> configure,
            object envelope);
    }
}