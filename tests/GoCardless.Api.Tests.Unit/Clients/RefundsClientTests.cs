﻿using Flurl.Http.Testing;
using GoCardlessApi.Refunds;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class RefundsClientTests
    {
        private IRefundsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new RefundsClient(configuration);
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
            TestDelegate test = () => new RefundsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_create_refund_options_not_provided()
        {
            // given
            CreateRefundOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_create_refund_endpoint()
        {
            // given
            var options = new CreateRefundOptions
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_id_not_provided(string id)
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
        public async Task calls_get_refund_endpoint()
        {
            // given
            var id = "RF12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds/RF12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task calls_get_refunds_endpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_get_refunds_options_not_provided()
        {
            // given
            GetRefundsOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_get_refunds_endpoint_using_options()
        {
            // given
            var options = new GetRefundsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_update_refund_options_not_provided()
        {
            // given
            UpdateRefundOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_update_refund_id_not_provided(string id)
        {
            // given
            var options = new UpdateRefundOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task calls_update_refund_endpoint()
        {
            // given
            var options = new UpdateRefundOptions
            {
                Id = "RF12345678"
            };

            // when
            await _subject.UpdateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds")
                .WithVerb(HttpMethod.Put);
        }
    }
}