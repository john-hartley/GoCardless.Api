using Flurl.Http.Testing;
using GoCardlessApi.Core;
using GoCardlessApi.Subscriptions;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit
{
    public class SubscriptionsClientTests
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
        public async Task CallsAllSubscriptionsEndpoint()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            await subject.AllAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void AllSubscriptionsRequestIsNullThrows()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            AllSubscriptionsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsAllSubscriptionsEndpointUsingRequest()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            var request = new AllSubscriptionsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.AllAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void CancelSubscriptionRequestIsNullThrows()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            CancelSubscriptionRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void CancelSubscriptionRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            var request = new CancelSubscriptionRequest();

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
            var subject = new SubscriptionsClient(_clientConfiguration);

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
            var subject = new SubscriptionsClient(_clientConfiguration);

            CreateSubscriptionRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsCreateSubscriptionEndpoint()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            var request = new CreateSubscriptionRequest();

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
        public void SubscriptionIdIsNullOrWhiteSpaceThrows(string subscriptionId)
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(subscriptionId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(subscriptionId)));
        }

        [Test]
        public async Task CallsIndividualSubscriptionsEndpoint()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);
            var subscriptionId = "SB12345678";

            // when
            await subject.ForIdAsync(subscriptionId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions/SB12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateSubscriptionRequestIsNullThrows()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            UpdateSubscriptionRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void UpdateSubscriptionRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            var request = new UpdateSubscriptionRequest();

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
            var subject = new SubscriptionsClient(_clientConfiguration);

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