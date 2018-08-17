using System;

namespace GoCardlessApi.Tests.Integration
{
    public abstract class IntegrationTest
    {
        protected readonly string _accessToken;

        public IntegrationTest()
        {
            _accessToken = Environment.GetEnvironmentVariable("GoCardlessAccessToken");
        }
    }
}