using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.CustomerNotifications;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class CustomerNotificationsClientTests
    {
        private ClientConfiguration _clientConfiguration;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            _clientConfiguration = ClientConfiguration.ForLive("accesstoken");
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
        public void CustomerNotificationIdIsNullOrWhiteSpaceThrows(string customerNotificationId)
        {
            // given
            var subject = new CustomerNotificationsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.HandleAsync(customerNotificationId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(customerNotificationId)));
        }

        [Test]
        public async Task CallsHandleCustomerNotificationsEndpoint()
        {
            // given
            var subject = new CustomerNotificationsClient(_clientConfiguration);
            var customerNotifcationId = "PCN12345678";

            // when
            await subject.HandleAsync(customerNotifcationId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_notifications")
                .WithVerb(HttpMethod.Post);
        }
    }
}