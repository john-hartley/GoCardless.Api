using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;

namespace GoCardless.Api.Tests.Integration.TestHelpers
{
    public abstract class IntegrationTest
    {
        protected readonly string _accessToken;
        protected readonly IApiClient _apiClient;
        protected readonly ResourceFactory _resourceFactory;

        internal IntegrationTest()
        {
            _accessToken = System.Environment.GetEnvironmentVariable("GoCardlessAccessToken");
            var configuration = ClientConfiguration.ForSandbox(_accessToken);
            _apiClient = new ApiClient(configuration);
            _resourceFactory = new ResourceFactory(_apiClient);
        }
    }
}