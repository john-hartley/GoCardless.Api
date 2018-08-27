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

        internal async Task<TResponse> GetAsync<TResponse>(params string[] pathSegments)
        {
            try
            {
                return await BaseRequest()
                    .AppendPathSegments(pathSegments)
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
            object envelope, 
            params string[] pathSegments)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            try
            {
                return await BaseRequest()
                    .AppendPathSegments(pathSegments)
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

        internal async Task<TResponse> PostAsync<TRequest, TResponse>(
            string[] pathSegments)
        {
            try
            {
                return await BaseRequest()
                    .AppendPathSegments(pathSegments)
                    .PostJsonAsync(new { })
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
            object envelope,
            string[] pathSegments)
        {
            return PostAsync<TRequest, TResponse>(envelope, null, pathSegments);
        }

        internal async Task<TResponse> PostAsync<TRequest, TResponse>(
            object envelope,
            string idempotencyKey,
            string[] pathSegments)
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
                    .AppendPathSegments(pathSegments)
                    .PostJsonAsync(envelope)
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