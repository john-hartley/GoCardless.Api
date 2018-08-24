using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
    {
        private readonly ClientConfiguration _configuration;

        public SubscriptionsClient(ClientConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public Task<SubscriptionsResponse> AllAsync()
        {
            return GetAsync<SubscriptionsResponse>("subscriptions");
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

        public Task<SubscriptionResponse> ForIdAsync(string subscriptionId)
        {
            return GetAsync<SubscriptionResponse>("subscriptions", subscriptionId);
        }

        public Task<UpdateSubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request)
        {
            return PutAsync<UpdateSubscriptionRequest, UpdateSubscriptionResponse>(
                new { subscriptions = request },
                "subscriptions", 
                request.Id);
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