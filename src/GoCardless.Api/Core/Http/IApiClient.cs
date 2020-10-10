﻿using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public interface IApiClient
    {
        Task<TResponse> GetAsync<TResponse>(Action<IFlurlRequest> configure);
        Task<TResponse> PostAsync<TResponse>(string relativeEndpoint, object envelope, Action<IFlurlRequest> configure);
        Task<TResponse> PutAsync<TResponse>(object envelope, Action<IFlurlRequest> configure);
    }
}