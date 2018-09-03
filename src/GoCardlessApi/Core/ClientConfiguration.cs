using System;
using System.Collections.Generic;

namespace GoCardlessApi.Core
{
    public class ClientConfiguration
    {
        public static ClientConfiguration ForLive(string accessToken)
        {
            return new ClientConfiguration(true, accessToken);
        }

        public static ClientConfiguration ForSandbox(string accessToken)
        {
            return new ClientConfiguration(false, accessToken);
        }

        private ClientConfiguration(bool live, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(accessToken));
            }

            BaseUri = live
                ? "https://api.gocardless.com/"
                : "https://api-sandbox.gocardless.com/";

            AccessToken = accessToken;

            Headers = new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {AccessToken}",
                ["GoCardless-Version"] = "2015-07-06",
            };
        }
        
        public string BaseUri { get; }
        public string AccessToken { get; }
        public IReadOnlyDictionary<string, string> Headers { get; }
    }
}