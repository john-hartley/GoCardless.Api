﻿using Flurl.Http.Testing;
using GoCardless.Api.Core.Http;
using GoCardless.Api.Payouts;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class PayoutsClientTests
    {
        private IPayoutsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken"));
            _subject = new PayoutsClient(apiClient);
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
            TestDelegate test = () => new PayoutsClient(apiClient);

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
            TestDelegate test = () => new PayoutsClient(apiClientConfiguration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClientConfiguration)));
        }

        [Test]
        public async Task CallsGetPayoutsEndpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payouts")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetPayoutsOptionsIsNullThrows()
        {
            // given
            GetPayoutsOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetPayoutsEndpointUsingOptions()
        {
            // given
            var options = new GetPayoutsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payouts?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            // when
            AsyncTestDelegate test = () => _subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualPayoutsEndpoint()
        {
            // given
            var id = "PO12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payouts/PO12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}