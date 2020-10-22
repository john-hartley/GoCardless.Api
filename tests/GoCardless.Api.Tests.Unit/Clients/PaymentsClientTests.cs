using Flurl.Http.Testing;
using GoCardlessApi.Payments;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class PaymentsClientTests
    {
        private IPaymentsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new PaymentsClient(configuration);
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
            TestDelegate test = () => new PaymentsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_cancel_payment_options_not_provided()
        {
            // given
            CancelPaymentOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_cancel_payment_id_not_provided(string id)
        {
            // given
            var options = new CancelPaymentOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task calls_cancel_payment_endpoint()
        {
            // given
            var options = new CancelPaymentOptions
            {
                Id = "PM12345678"
            };

            // when
            await _subject.CancelAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments/PM12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void throws_when_create_payment_options_not_provided()
        {
            // given
            CreatePaymentOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_create_payment_endpoint()
        {
            // given
            var options = new CreatePaymentOptions
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments")
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
        public async Task calls_get_payment_endpoint()
        {
            // given
            var id = "PM12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments/PM12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task calls_get_payments_endpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_get_payment_options_not_provided()
        {
            // given
            GetPaymentsOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_get_payments_endpoint_using_options()
        {
            // given
            var options = new GetPaymentsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_retry_payment_options_not_provided()
        {
            // given
            RetryPaymentOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.RetryAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_retry_payment_id_not_provided(string id)
        {
            // given
            var options = new RetryPaymentOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.RetryAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task calls_retry_payment_endpoint()
        {
            // given
            var options = new RetryPaymentOptions
            {
                Id = "PM12345678"
            };

            // when
            await _subject.RetryAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments/PM12345678/actions/retry")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void throws_when_update_payment_options_not_provided()
        {
            // given
            UpdatePaymentOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_update_payment_id_not_provided(string id)
        {
            // given
            var options = new UpdatePaymentOptions
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
        public async Task calls_update_payment_endpoint()
        {
            // given
            var options = new UpdatePaymentOptions
            {
                Id = "PM12345678"
            };

            // when
            await _subject.UpdateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments")
                .WithVerb(HttpMethod.Put);
        }
    }
}