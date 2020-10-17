using Flurl.Http.Testing;
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
        private ISubscriptionsClient subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = ApiClientConfiguration.ForLive("accesstoken", false);
            subject = new SubscriptionsClient(configuration);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void ConfigurationIsNullThrows()
        {
            // given
            ApiClientConfiguration configuration = null;

            // when
            TestDelegate test = () => new SubscriptionsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void CancelSubscriptionOptionsIsNullThrows()
        {
            // given
            CancelSubscriptionOptions options = null;

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CancelSubscriptionOptionsIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var options = new CancelSubscriptionOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task CallsCancelSubscriptionEndpoint()
        {
            // given
            var options = new CancelSubscriptionOptions
            {
                Id = "SB12345678"
            };

            // when
            await subject.CancelAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions/SB12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void CreateSubscriptionOptionsIsNullThrows()
        {
            // given
            CreateSubscriptionOptions options = null;

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
            var options = new CreateSubscriptionOptions
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await subject.CreateAsync(options);

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
            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetSubscriptionsOptionsIsNullThrows()
        {
            // given
            GetSubscriptionsOptions options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetSubscriptionsEndpointUsingOptions()
        {
            // given
            var options = new GetSubscriptionsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateSubscriptionOptionsIsNullThrows()
        {
            // given
            UpdateSubscriptionOptions options = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateSubscriptionOptionsIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var options = new UpdateSubscriptionOptions
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
        public async Task CallsUpdateSubscriptionEndpoint()
        {
            // given
            var options = new UpdateSubscriptionOptions
            {
                Id = "SB12345678"
            };

            // when
            await subject.UpdateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/subscriptions")
                .WithVerb(HttpMethod.Put);
        }
    }
}