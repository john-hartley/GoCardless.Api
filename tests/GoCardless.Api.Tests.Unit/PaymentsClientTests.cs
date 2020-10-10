using Flurl.Http.Testing;
using GoCardless.Api.Core.Http;
using GoCardless.Api.Payments;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class PaymentsClientTests
    {
        private IApiClient _apiClient;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            _apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken"));
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void CancelPaymentRequestIsNullThrows()
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            CancelPaymentOptions options = null;

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CancelPaymentRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            var options = new CancelPaymentOptions
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
        public async Task CallsCancelPaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            var request = new CancelPaymentOptions
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
            var subject = new PaymentsClient(_apiClient);

            CreatePaymentOptions options = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreatePaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            var request = new CreatePaymentOptions
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
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualPaymentsEndpoint()
        {
            // given
            var subject = new PaymentsClient(_apiClient);
            var id = "PM12345678";

            // when
            await subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments/PM12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetPaymentsEndpoint()
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetPaymentsRequestIsNullThrows()
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            GetPaymentsOptions options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetPaymentsEndpointUsingRequest()
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            var request = new GetPaymentsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payments?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void RetryPaymentRequestIsNullThrows()
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            RetryPaymentOptions options = null;

            // when
            AsyncTestDelegate test = () => subject.RetryAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void RetryPaymentRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            var request = new RetryPaymentOptions
            {
                Id = id
            };

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
            var subject = new PaymentsClient(_apiClient);

            var request = new RetryPaymentOptions
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
            var subject = new PaymentsClient(_apiClient);

            UpdatePaymentOptions options = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdatePaymentRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            var options = new UpdatePaymentOptions
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
        public async Task CallsUpdatePaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_apiClient);

            var request = new UpdatePaymentOptions
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