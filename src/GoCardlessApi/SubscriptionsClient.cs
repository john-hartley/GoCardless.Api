using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class SubscriptionsClient : ISubscriptionsClient
    {
        private readonly ClientConfiguration _configuration;

        public SubscriptionsClient(ClientConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SubscriptionsResponse> AllAsync()
        {            
            return await _configuration.BaseUri
                .WithHeaders(_configuration.Headers)
                .AppendPathSegment("subscriptions")
                .GetJsonAsync<SubscriptionsResponse>()
                .ConfigureAwait(false);
        }

        public async Task<CreateSubscriptionResponse> CreateAsync(CreateSubscriptionRequest request)
        {
            var envelope = new { subscriptions = request };
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            try
            {
                var idempotencyKey = Guid.NewGuid().ToString();
                return await _configuration.BaseUri
                    .WithHeaders(_configuration.Headers)
                    .WithHeader("Idempotency-Key", idempotencyKey)
                    .AppendPathSegment("subscriptions")
                    .PostJsonAsync(envelope)
                    .ReceiveJson<CreateSubscriptionResponse>();
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
            }

            return null;
        }

        public async Task<SubscriptionResponse> ForIdAsync(string subscriptionId)
        {
            return await _configuration.BaseUri
                .WithHeaders(_configuration.Headers)
                .AppendPathSegments("subscriptions", subscriptionId)
                .GetJsonAsync<SubscriptionResponse>()
                .ConfigureAwait(false);
        }

        public async Task<UpdateSubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request)
        {
            var envelope = new { subscriptions = request };
            Debug.WriteLine(JsonConvert.SerializeObject(envelope));

            try
            {
                var response = await _configuration.BaseUri
                    .WithHeaders(_configuration.Headers)
                    .AppendPathSegments("subscriptions", request.Id)
                    .PutJsonAsync(envelope)
                    .ReceiveJson<UpdateSubscriptionResponse>();
                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
            }

            return null;
        }

        public async Task<CancelSubscriptionResponse> CancelAsync(CancelSubscriptionRequest request)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(request));

            try
            {
                var response = await _configuration.BaseUri
                    .WithHeaders(_configuration.Headers)
                    .AppendPathSegments("subscriptions", request.Id, "actions", "cancel")
                    .PostJsonAsync(request)
                    .ReceiveJson<CancelSubscriptionResponse>();
                return response;
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync();
            }

            return null;
        }
    }
}