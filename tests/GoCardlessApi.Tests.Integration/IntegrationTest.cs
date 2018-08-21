namespace GoCardlessApi.Tests.Integration
{
    public abstract class IntegrationTest
    {
        protected readonly string _accessToken;

        public IntegrationTest()
        {
            _accessToken = System.Environment.GetEnvironmentVariable("GoCardlessAccessToken");
        }
    }
}