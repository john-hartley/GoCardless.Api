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
        private ICustomerNotificationsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken"));
            _subject = new CustomerNotificationsClient(apiClient);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void ApiClientIsNullThrows()
        {
            // given
            IApiClient apiClient = null;

            // when
            TestDelegate test = () => new CustomerNotificationsClient(apiClient);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClient)));
        }

        [Test]
        public void ApiClientConfigurationIsNullThrows()
        {
            // given
            ApiClientConfiguration apiClientConfiguration = null;

            // when
            TestDelegate test = () => new CustomerNotificationsClient(apiClientConfiguration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClientConfiguration)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            // when
            AsyncTestDelegate test = () => _subject.HandleAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsHandleCustomerNotificationsEndpoint()
        {
            // given
            var id = "PCN12345678";

            // when
            await _subject.HandleAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_notifications")
                .WithVerb(HttpMethod.Post);
        }
    }
}