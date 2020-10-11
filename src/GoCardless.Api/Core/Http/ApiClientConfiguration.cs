using System;
using System.Collections.Generic;

namespace GoCardless.Api.Core.Http
{
    public class ApiClientConfiguration
    {
        public static ApiClientConfiguration ForLive(string accessToken, bool throwOnConflict)
        {
            return new ApiClientConfiguration(accessToken, true, throwOnConflict);
        }

        public static ApiClientConfiguration ForSandbox(string accessToken, bool throwOnConflict)
        {
            return new ApiClientConfiguration(accessToken, false, throwOnConflict);
        }

        private ApiClientConfiguration(string accessToken, bool live, bool throwOnConflict)
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

            ThrowOnConflict = throwOnConflict;
        }

        public string AccessToken { get; }
        public string BaseUri { get; }
        public IDictionary<string, string> Headers { get; }
        public bool ThrowOnConflict { get; }
    }
}