using GoCardless.Api.Core;

namespace GoCardless.Api.Tests.Integration.TestHelpers
{
    public abstract class IntegrationTest
    {
        protected readonly string _accessToken;
        protected readonly ClientConfiguration _clientConfiguration;
        protected readonly ResourceFactory _resourceFactory;

        public IntegrationTest()
        {
            _accessToken = System.Environment.GetEnvironmentVariable("GoCardlessAccessToken");
            _clientConfiguration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_clientConfiguration);
        }
    }
}