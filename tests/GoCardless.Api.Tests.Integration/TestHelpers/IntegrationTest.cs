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
            var apiClientConfiguration = ApiClientConfiguration.ForSandbox(_accessToken, false);
            _apiClient = new ApiClient(apiClientConfiguration);
            _resourceFactory = new ResourceFactory(_apiClient);
        }
    }
}