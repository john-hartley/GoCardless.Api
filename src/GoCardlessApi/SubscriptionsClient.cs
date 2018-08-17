using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace GoCardlessApi
{
    public class SubscriptionsClient
    {
        private readonly string _accessToken;

        public SubscriptionsClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<SubscriptionsResponse> AllAsync()
        {            
            return await "https://api-sandbox.gocardless.com/"
                .WithHeader("Authorization", $"Bearer {_accessToken}")
                .WithHeader("GoCardless-Version", "2015-07-06")
                .AppendPathSegment("subscriptions")
                .GetJsonAsync<SubscriptionsResponse>();
        }

        public async Task<SubscriptionResponse> ForIdAsync(string subscriptionId)
        {
            return await "https://api-sandbox.gocardless.com/"
                .WithHeader("Authorization", $"Bearer {_accessToken}")
                .WithHeader("GoCardless-Version", "2015-07-06")
                .AppendPathSegments("subscriptions", subscriptionId)
                .GetJsonAsync<SubscriptionResponse>();
        }
    }
}