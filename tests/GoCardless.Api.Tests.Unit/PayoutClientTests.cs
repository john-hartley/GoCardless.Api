﻿using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Payouts;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class PayoutClientTests
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

        [Test]
        public void AllPayoutsRequestIsNullThrows()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);

            AllPayoutsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsAllPayoutsEndpointUsingRequest()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);

            var request = new AllPayoutsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.AllAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payouts?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsAllPayoutsEndpoint()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);

            // when
            await subject.AllAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payouts")
                .WithVerb(HttpMethod.Get);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void PayoutIdIsNullOrWhiteSpaceThrows(string payoutId)
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(payoutId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(payoutId)));
        }

        [Test]
        public async Task CallsIndividualPayoutsEndpoint()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);
            var PayoutId = "PO12345678";

            // when
            await subject.ForIdAsync(PayoutId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payouts/PO12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}