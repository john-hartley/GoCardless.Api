using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public class ApiClient : IApiClient
    {
        private readonly NewtonsoftJsonSerializer _newtonsoftJsonSerializer;

        public ApiClient(ClientConfiguration configuration)
        {
            Configuration = configuration;

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

        public ClientConfiguration Configuration { get; }

        public async Task<TResponse> GetAsync<TResponse>(Action<IFlurlRequest> configure)
        {
            try
            {
                var request = BaseRequest();
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

        public async Task<TResponse> PutAsync<TResponse>(
            string relativeEndpoint,
            object envelope)
        {
            try
            {
                return await BaseRequest()
                    .AppendPathSegment(relativeEndpoint)
                    .PutJsonAsync(envelope)
                    .ReceiveJson<TResponse>();
            }
            catch (FlurlHttpException ex)
            {
                throw await ex.CreateApiExceptionAsync();
            }
        }

        public Task<TResponse> PostAsync<TResponse>(
            string relativeEndpoint)
        {
            return PostAsync<TResponse>(relativeEndpoint, null, null, null);
        }

        public Task<TResponse> PostAsync<TResponse>(
            string relativeEndpoint,
            object envelope,
            IReadOnlyDictionary<string, string> customHeaders = null)
        {
            return PostAsync<TResponse>(relativeEndpoint, envelope, null, customHeaders);
        }

        public async Task<TResponse> PostAsync<TResponse>(
            string relativeEndpoint,
            object envelope,
            string idempotencyKey,
            IReadOnlyDictionary<string, string> customHeaders = null)
        {
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
                var apiException = await ex.CreateApiExceptionAsync();
                if (apiException.Code == (int)HttpStatusCode.Conflict)
                {
                    var conflictingResourceId = ConflictingResourceIdFrom(apiException.Errors);
                    if (!string.IsNullOrWhiteSpace(conflictingResourceId))
                    {
                        var endpoint = $"{relativeEndpoint}/{conflictingResourceId}";
                        return await GetAsync<TResponse>(req => req.AppendPathSegment(endpoint));
                    }
                }

                throw apiException;
            }
        }

        private IFlurlRequest BaseRequest()
        {
            return Configuration.BaseUri
                .WithHeaders(Configuration.Headers)
                .ConfigureRequest(x => x.JsonSerializer = _newtonsoftJsonSerializer);
        }

        private string ConflictingResourceIdFrom(IEnumerable<Error> errors)
        {
            var bankAccountExists = errors.SingleOrDefault(x => x.Reason == "bank_account_exists");
            if (bankAccountExists != null)
            {
                if (bankAccountExists.Links.ContainsKey("creditor_bank_account"))
                {
                    return bankAccountExists.Links["creditor_bank_account"];
                }
                if (bankAccountExists.Links.ContainsKey("customer_bank_account"))
                {
                    return bankAccountExists.Links["customer_bank_account"];
                }
            }

            return errors
                .SingleOrDefault(x => x.Reason == "idempotent_creation_conflict")
                ?.Links
                ?.SingleOrDefault(x => x.Key == "conflicting_resource_id").Value;
        }
    }
}