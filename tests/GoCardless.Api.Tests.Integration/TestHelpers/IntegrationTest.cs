using GoCardless.Api.Core.Http;

namespace GoCardless.Api.Tests.Integration.TestHelpers
{
    public abstract class IntegrationTest
    {
        protected readonly string _accessToken;
        protected readonly ApiClientConfiguration _configuration;
        protected readonly ResourceFactory _resourceFactory;

        internal IntegrationTest()
        {
            _accessToken = System.Environment.GetEnvironmentVariable("GoCardlessAccessToken");
            _configuration = ApiClientConfiguration.ForSandbox(_accessToken, false);
            _resourceFactory = new ResourceFactory(_configuration);
        }
    }
}