﻿using System;

namespace GoCardlessApi
{
    public class ClientConfiguration
    {
        private readonly Uri _baseUri;

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
            _baseUri = live
                ? new Uri("https://api.gocardless.com")
                : new Uri("https://api-sandbox.gocardless.com");

            AccessToken = accessToken;
        }
        
        public string BaseUri => _baseUri.ToString();
        public string AccessToken { get; }
    }
}