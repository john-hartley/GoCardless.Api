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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);
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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

            UpdateRefundRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateRefundRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

            var request = new UpdateRefundRequest
            {
                Id = id
            };

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
            var subject = new RefundsClient(_apiClient, _apiClient.Configuration);

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