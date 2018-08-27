using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GoCardlessApi.Core
{
    public abstract class ApiClientBase
    {
        private readonly ClientConfiguration _configuration;

        internal ApiClientBase(ClientConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal async Task<TResponse> GetAsync<TResponse>(string relativeEndpoint)
        {
            try
            {
                return await BaseRequest()
                    .AppendPathSegment(relativeEndpoint)
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
            return PostAsync<TRequest, TResponse>(relativeEndpoint, null, null);
        }

        internal Task<TResponse> PostAsync<TRequest, TResponse>(
            string relativeEndpoint,
            object envelope)
        {
            return PostAsync<TRequest, TResponse>(relativeEndpoint, envelope, null);
        }

        internal async Task<TResponse> PostAsync<TRequest, TResponse>(
            string relativeEndpoint,
            object envelope,
            string idempotencyKey)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            var request = BaseRequest();

            if (!string.IsNullOrWhiteSpace(idempotencyKey))
            {
                request.WithHeader("Idempotency-Key", idempotencyKey);
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
            return _configuration.BaseUri.WithHeaders(_configuration.Headers);
        }
    }
}