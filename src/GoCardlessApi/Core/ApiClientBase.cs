using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GoCardlessApi.Core
{
    public abstract class ApiClientBase
    {
        private readonly ClientConfiguration _configuration;
        private readonly NewtonsoftJsonSerializer _newtonsoftJsonSerializer;

        internal ApiClientBase(ClientConfiguration configuration)
        {
            _configuration = configuration;

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
        }

        internal async Task<TResponse> GetAsync<TResponse>(
            string relativeEndpoint,
            IReadOnlyDictionary<string, object> queryParams = null)
        {
            try
            {
                return await BaseRequest()
                    .AppendPathSegment(relativeEndpoint)
                    .SetQueryParams(queryParams)
                    .GetJsonAsync<TResponse>()
                    .ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                Debug.WriteLine(JsonConvert.SerializeObject(error));
            }

            return default(TResponse);
        }

        internal async Task<TResponse> PutAsync<TRequest, TResponse>(
            string relativeEndpoint,
            object envelope)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            try
            {
                return await BaseRequest()
                    .AppendPathSegment(relativeEndpoint)
                    .PutJsonAsync(envelope)
                    .ReceiveJson<TResponse>();
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                Debug.WriteLine(JsonConvert.SerializeObject(error));
            }

            return default(TResponse);
        }

        internal Task<TResponse> PostAsync<TRequest, TResponse>(
            string relativeEndpoint)
        {
            return PostAsync<TRequest, TResponse>(relativeEndpoint, null, null, null);
        }

        internal Task<TResponse> PostAsync<TRequest, TResponse>(
            string relativeEndpoint,
            object envelope,
            IReadOnlyDictionary<string, string> customHeaders = null)
        {
            return PostAsync<TRequest, TResponse>(relativeEndpoint, envelope, null, customHeaders);
        }

        internal async Task<TResponse> PostAsync<TRequest, TResponse>(
            string relativeEndpoint,
            object envelope,
            string idempotencyKey,
            IReadOnlyDictionary<string, string> customHeaders = null)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            var request = BaseRequest();

            if (!string.IsNullOrWhiteSpace(idempotencyKey))
            {
                request.WithHeader("Idempotency-Key", idempotencyKey);
            }

            if (customHeaders?.Count > 0)
            {
                request.WithHeaders(customHeaders);
            }

            try
            {
                return await request
                    .AppendPathSegment(relativeEndpoint)
                    .PostJsonAsync(envelope ?? new { })
                    .ReceiveJson<TResponse>();
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync();
                Debug.WriteLine(JsonConvert.SerializeObject(error));
            }

            return default(TResponse);
        }

        private IFlurlRequest BaseRequest()
        {
            return _configuration.BaseUri
                .WithHeaders(_configuration.Headers)
                .ConfigureRequest(x => x.JsonSerializer = _newtonsoftJsonSerializer);
        }
    }
}