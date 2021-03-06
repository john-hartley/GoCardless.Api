﻿using Flurl.Http.Testing;
using GoCardlessApi.CustomerNotifications;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class CustomerNotificationsClientTests
    {
        private ICustomerNotificationsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new CustomerNotificationsClient(configuration);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void throws_when_configuration_not_provided()
        {
            // given
            GoCardlessConfiguration configuration = null;

            // when
            TestDelegate test = () => new CustomerNotificationsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_handle_customer_notification_options_not_provided()
        {
            // given
            HandleCustomerNotificationOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.HandleAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_handle_customer_notification_id_not_provided(string id)
        {
            // given
            var options = new HandleCustomerNotificationOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.HandleAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task calls_handle_customer_notifications_endpoint()
        {
            // given
            var options = new HandleCustomerNotificationOptions
            {
                Id = "PCN12345678"
            };

            // when
            await _subject.HandleAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_notifications/PCN12345678/actions/handle")
                .WithVerb(HttpMethod.Post);
        }
    }
}