using Flurl.Http.Testing;
using GoCardlessApi.Subscriptions;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class SubscriptionsClientTests
    {
        private ISubscriptionsClient subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            subject = new SubscriptionsClient(configuration);
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
            TestDelegate test = () => new SubscriptionsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_cancel_subscription_options_not_provided()
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
        public void throws_when_cancel_subscription_id_not_provided(string id)
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
        public async Task calls_cancel_subscription_endpoint()
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
        public void throws_when_create_subscription_options_not_provided()
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
        public async Task calls_create_subscription_endpoint()
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
        public void throws_when_id_not_provided(string id)
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
        public async Task calls_get_subscription_endpoint()
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
        public async Task calls_get_subscriptions_endpoint()
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
        public void throws_when_get_subscriptions_options_not_provided()
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
        public async Task calls_get_subscriptions_endpoint_using_options()
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
        public void throws_when_update_subscription_options_not_provided()
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
        public void throws_when_update_subscription_id_not_provided(string id)
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
        public async Task calls_update_subscription_endpoint()
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