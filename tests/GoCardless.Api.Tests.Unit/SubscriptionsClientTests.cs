using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.Subscriptions;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class SubscriptionsClientTests
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
        public void CancelSubscriptionRequestIsNullThrows()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            CancelSubscriptionRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CancelSubscriptionRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            var request = new CancelSubscriptionRequest
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsCancelSubscriptionEndpoint()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            var request = new CancelSubscriptionRequest
            {
                Id = "SB12345678"
            };

            // when
            await subject.CancelAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions/SB12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void CreateSubscriptionRequestIsNullThrows()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            CreateSubscriptionRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateSubscriptionEndpoint()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            var request = new CreateSubscriptionRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await subject.CreateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualSubscriptionsEndpoint()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);
            var id = "SB12345678";

            // when
            await subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions/SB12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetSubscriptionsEndpoint()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetSubscriptionsRequestIsNullThrows()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            GetSubscriptionsRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetSubscriptionsEndpointUsingRequest()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            var request = new GetSubscriptionsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateSubscriptionRequestIsNullThrows()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            UpdateSubscriptionRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateSubscriptionRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            var request = new UpdateSubscriptionRequest
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
        public async Task CallsUpdateSubscriptionEndpoint()
        {
            // given
            var subject = new SubscriptionsClient(_apiClient);

            var request = new UpdateSubscriptionRequest
            {
                Id = "SB12345678"
            };

            // when
            await subject.UpdateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions")
                .WithVerb(HttpMethod.Put);
        }
    }
}