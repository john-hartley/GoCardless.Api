using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.Refunds;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class RefundsClientTests
    {
        private IApiClient _apiClient;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            _apiClient = new ApiClient(ClientConfiguration.ForLive("accesstoken"));
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void CreateRefundRequestIsNullThrows()
        {
            // given
            var subject = new RefundsClient(_apiClient);

            CreateRefundRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateRefundEndpoint()
        {
            // given
            var subject = new RefundsClient(_apiClient);

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
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new RefundsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualRefundsEndpoint()
        {
            // given
            var subject = new RefundsClient(_apiClient);
            var id = "RF12345678";

            // when
            await subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds/RF12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetRefundsEndpoint()
        {
            // given
            var subject = new RefundsClient(_apiClient);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetRefundsRequestIsNullThrows()
        {
            // given
            var subject = new RefundsClient(_apiClient);

            GetRefundsRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetRefundsEndpointUsingRequest()
        {
            // given
            var subject = new RefundsClient(_apiClient);

            var request = new GetRefundsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/refunds?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateRefundRequestIsNullThrows()
        {
            // given
            var subject = new RefundsClient(_apiClient);

            UpdateRefundRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateRefundRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new RefundsClient(_apiClient);

            var options = new UpdateRefundRequest
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task CallsUpdateRefundEndpoint()
        {
            // given
            var subject = new RefundsClient(_apiClient);

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