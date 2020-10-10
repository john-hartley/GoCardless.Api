using Flurl.Http.Testing;
using GoCardless.Api.Core.Http;
using GoCardless.Api.CustomerNotifications;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class CustomerNotificationsClientTests
    {
        private IApiClient _apiClient;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            _apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken"));
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new CustomerNotificationsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.HandleAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsHandleCustomerNotificationsEndpoint()
        {
            // given
            var subject = new CustomerNotificationsClient(_apiClient);
            var id = "PCN12345678";

            // when
            await subject.HandleAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_notifications")
                .WithVerb(HttpMethod.Post);
        }
    }
}