using Flurl.Http.Testing;
using GoCardless.Api.Core;
using GoCardless.Api.Refunds;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class RefundsClientTests
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
        public async Task CallsAllRefundsEndpoint()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            // when
            await subject.AllAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void AllRefundsRequestIsNullThrows()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            AllRefundsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsAllRefundsEndpointUsingRequest()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            var request = new AllRefundsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.AllAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void CreateRefundRequestIsNullThrows()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            CreateRefundRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsCreateRefundEndpoint()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            var request = new CreateRefundRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await subject.CreateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void RefundIdIsNullOrWhiteSpaceThrows(string refundId)
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(refundId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(refundId)));
        }

        [Test]
        public async Task CallsIndividualRefundsEndpoint()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);
            var RefundId = "RF12345678";

            // when
            await subject.ForIdAsync(RefundId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds/RF12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateRefundRequestIsNullThrows()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            UpdateRefundRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void UpdateRefundRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            var request = new UpdateRefundRequest();

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsUpdateRefundEndpoint()
        {
            // given
            var subject = new RefundsClient(_clientConfiguration);

            var request = new UpdateRefundRequest
            {
                Id = "RF12345678"
            };

            // when
            await subject.UpdateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds")
                .WithVerb(HttpMethod.Put);
        }
    }
}