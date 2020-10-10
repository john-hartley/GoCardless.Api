using System;
using System.Collections.Generic;

namespace GoCardless.Api.Core.Http
{
    public class ApiClientConfiguration
    {
        public static ApiClientConfiguration ForLive(string accessToken)
        {
            return new ApiClientConfiguration(true, accessToken);
        }

        public static ApiClientConfiguration ForSandbox(string accessToken)
        {
            return new ApiClientConfiguration(false, accessToken);
        }

        private ApiClientConfiguration(bool live, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(accessToken));
            }

            AccessToken = accessToken;

            BaseUri = live
                ? "https://api.gocardless.com/"
                : "https://api-sandbox.gocardless.com/";

            Headers = new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {AccessToken}",
                ["GoCardless-Version"] = "2015-07-06",
            };
        }

        public string BaseUri { get; }
        public string AccessToken { get; }
        public IDictionary<string, string> Headers { get; }
    }
}