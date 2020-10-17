using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using GoCardless.Api.Core.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public class ApiClient : IApiClient
    {
        private readonly NewtonsoftJsonSerializer _newtonsoftJsonSerializer;
        private readonly ApiClientConfiguration _configuration;

        public ApiClient(ApiClientConfiguration configuration)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            _newtonsoftJsonSerializer = new NewtonsoftJsonSerializer(jsonSerializerSettings);
            _configuration = configuration;
        }

        public async Task<TResponse> RequestAsync<TResponse>(
            Func<IFlurlRequest, Task<TResponse>> action)
        {
            var request = Request();
            return await SendAsync(request, action).ConfigureAwait(false);
        }

        public async Task<TResponse> IdempotentRequestAsync<TResponse>(
            string idempotencyKey,
            Func<IFlurlRequest, Task<TResponse>> action)
        {
            var request = IdempotentRequest(idempotencyKey);
            return await SendAsync(request, action).ConfigureAwait(false);
        }

        private async Task<TResponse> SendAsync<TResponse>(
            IFlurlRequest request, 
            Func<IFlurlRequest, Task<TResponse>> action)
        {
            try
            {
                return await action(request).ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                var apiException = await ex.CreateApiExceptionAsync().ConfigureAwait(false);
                if (apiException is ConflictingResourceException conflictingResourceException
                    && !_configuration.ThrowOnConflict)
                {
                    var uri = ex.Call.Request.RequestUri;
                    var conflictingResourceId = conflictingResourceException.ResourceId;

                    if (!string.IsNullOrWhiteSpace(conflictingResourceId)
                        && uri.Segments.Length >= 2)
                    {
                        return await RequestAsync(fetchOnConflictRequest =>
                        {
                            return fetchOnConflictRequest
                                .AppendPathSegments(uri.Segments[1], conflictingResourceId)
                                .GetJsonAsync<TResponse>();
                        }).ConfigureAwait(false);
                    }
                }

                throw apiException;
            }
        }

        private IFlurlRequest IdempotentRequest(string idempotencyKey)
        {
            return Request().WithHeader("Idempotency-Key", idempotencyKey);
        }

        private IFlurlRequest Request()
        {
            return _configuration.BaseUri
                .WithHeaders(_configuration.Headers)
                .ConfigureRequest(x => x.JsonSerializer = _newtonsoftJsonSerializer);
        }
    }
}