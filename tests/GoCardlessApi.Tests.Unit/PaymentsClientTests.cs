using Flurl.Http.Testing;
using GoCardless.Api.Core;
using GoCardless.Api.Payments;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class PaymentsClientTests
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
        public void AllPaymentsRequestIsNullThrows()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            AllPaymentsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsAllPaymentsEndpointUsingRequest()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var request = new AllPaymentsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.AllAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsAllPaymentsEndpoint()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            // when
            await subject.AllAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void CancelPaymentRequestIsNullThrows()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            CancelPaymentRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void CancelPaymentRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var request = new CancelPaymentRequest();

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsCancelPaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var request = new CancelPaymentRequest
            {
                Id = "PM12345678"
            };

            // when
            await subject.CancelAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments/PM12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void CreatePaymentRequestIsNullThrows()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            CreatePaymentRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsCreatePaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var request = new CreatePaymentRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await subject.CreateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void PaymentIdIsNullOrWhiteSpaceThrows(string paymentId)
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(paymentId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(paymentId)));
        }

        [Test]
        public async Task CallsIndividualPaymentsEndpoint()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);
            var paymentId = "PM12345678";

            // when
            await subject.ForIdAsync(paymentId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments/PM12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void RetryPaymentRequestIsNullThrows()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            RetryPaymentRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.RetryAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void RetryPaymentRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var request = new RetryPaymentRequest();

            // when
            AsyncTestDelegate test = () => subject.RetryAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsRetryPaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var request = new RetryPaymentRequest
            {
                Id = "PM12345678"
            };

            // when
            await subject.RetryAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments/PM12345678/actions/retry")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void UpdatePaymentRequestIsNullThrows()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            UpdatePaymentRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void UpdatePaymentRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var request = new UpdatePaymentRequest();

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsUpdatePaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var request = new UpdatePaymentRequest
            {
                Id = "PM12345678"
            };

            // when
            await subject.UpdateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments")
                .WithVerb(HttpMethod.Put);
        }
    }
}