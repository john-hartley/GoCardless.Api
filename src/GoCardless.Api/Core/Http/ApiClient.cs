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
        private readonly ApiClientConfiguration _apiClientConfiguration;

        public ApiClient(ApiClientConfiguration apiClientConfiguration)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            _newtonsoftJsonSerializer = new NewtonsoftJsonSerializer(jsonSerializerSettings);
            _apiClientConfiguration = apiClientConfiguration;
        }

        public async Task<TResponse> RequestAsync<TResponse>(Action<IFlurlRequest> configure)
        {
            try
            {
                var request = Request();
                configure(request);

                return await request
                    .GetJsonAsync<TResponse>()
                    .ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                throw await ex.CreateApiExceptionAsync();
            }
        }

        public async Task<TResponse> IdempotentAsync<TResponse>(
            Action<IFlurlRequest> configure,
            object envelope = null)
        {
            try
            {
                var request = Request();
                configure(request);

                return await request
                    .PostJsonAsync(envelope ?? new { })
                    .ReceiveJson<TResponse>();
            }
            catch (FlurlHttpException ex)
            {
                var apiException = await ex.CreateApiExceptionAsync();
                if (apiException is ConflictingResourceException conflictingResourceException
                    && !_apiClientConfiguration.ThrowOnConflict)
                {
                    var uri = ex.Call.Request.RequestUri;
                    var conflictingResourceId = conflictingResourceException.ResourceId;

                    if (!string.IsNullOrWhiteSpace(conflictingResourceId) 
                        && uri.Segments.Length >= 2)
                    {
                        return await RequestAsync<TResponse>(request =>
                        {
                            request.AppendPathSegments(uri.Segments[1], conflictingResourceId);
                        });
                    }
                }

                throw apiException;
            }
        }

        public async Task<TResponse> PutAsync<TResponse>(
            Action<IFlurlRequest> configure,
            object envelope)
        {
            try
            {
                var request = Request();
                configure(request);

                return await request
                    .PutJsonAsync(envelope)
                    .ReceiveJson<TResponse>();
            }
            catch (FlurlHttpException ex)
            {
                throw await ex.CreateApiExceptionAsync();
            }
        }

        private IFlurlRequest IdempotentRequest(string idempotencyKey)
        {
            return Request().WithHeader("Idempotency-Key", idempotencyKey);
        }

        private IFlurlRequest Request()
        {
            return _apiClientConfiguration.BaseUri
                .WithHeaders(_apiClientConfiguration.Headers)
                .ConfigureRequest(x => x.JsonSerializer = _newtonsoftJsonSerializer);
        }
    }
}