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
        private readonly ClientConfiguration _configuration;

        public ApiClient(ClientConfiguration configuration)
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
            _configuration = configuration;
        }

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

        public async Task<TResponse> PostAsync<TResponse>(
            // Todo: Check if I can use Uri in place of this.
            string relativeEndpoint,
            Action<IFlurlRequest> configure,
            object envelope = null)
        {
            var request = BaseRequest();
            configure(request);

            try
            {
                return await request
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

        public async Task<TResponse> PutAsync<TResponse>(
            Action<IFlurlRequest> configure,
            object envelope)
        {
            var request = BaseRequest();
            configure(request);

            try
            {
                return await request
                    .PutJsonAsync(envelope)
                    .ReceiveJson<TResponse>();
            }
            catch (FlurlHttpException ex)
            {
                throw await ex.CreateApiExceptionAsync();
            }
        }

        private IFlurlRequest BaseRequest()
        {
            return _configuration.BaseUri
                .WithHeaders(_configuration.Headers)
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