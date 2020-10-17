using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public interface IApiClient
    {
        Task<TResponse> RequestAsync<TResponse>(Func<IFlurlRequest, Task<TResponse>> action);
        Task<TResponse> IdempotentAsync<TResponse>(string idempotencyKey, Func<IFlurlRequest, Task<TResponse>> action);
        Task<TResponse> PutAsync<TResponse>(Action<IFlurlRequest> configure, object envelope);
    }
}