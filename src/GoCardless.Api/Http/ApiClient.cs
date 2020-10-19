using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using GoCardlessApi.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Http
{
    internal class ApiClient
    {
        private readonly NewtonsoftJsonSerializer _serializer;
        private readonly GoCardlessConfiguration _configuration;

        internal ApiClient(GoCardlessConfiguration configuration)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            _serializer = new NewtonsoftJsonSerializer(jsonSerializerSettings);
            _configuration = configuration;
        }

        internal async Task<TResponse> RequestAsync<TResponse>(
            Func<IFlurlRequest, Task<TResponse>> action)
        {
            var request = Request();
            return await SendAsync(request, action).ConfigureAwait(false);
        }

        internal async Task<TResponse> IdempotentRequestAsync<TResponse>(
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
                if (apiException is ConflictingResourceException
                    && !_configuration.ThrowOnConflict)
                {
                    var uri = ex.Call.Request.RequestUri;
                    var conflictingResourceId = apiException.ResourceId;

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
                .ConfigureRequest(x => x.JsonSerializer = _serializer);
        }
    }
}