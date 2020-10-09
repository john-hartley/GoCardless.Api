using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
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
            _apiClient = new ApiClient(ClientConfiguration.ForLive("accesstoken"));
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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            CancelPaymentRequest options = null;

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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            var request = new CancelPaymentRequest
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
        public async Task CallsCancelPaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            CreatePaymentRequest options = null;

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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

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
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);
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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            GetPaymentsRequest options = null;

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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            var request = new GetPaymentsRequest
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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            RetryPaymentRequest options = null;

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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            var request = new RetryPaymentRequest
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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

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
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            UpdatePaymentRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdatePaymentRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

            var request = new UpdatePaymentRequest
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
        public async Task CallsUpdatePaymentEndpoint()
        {
            // given
            var subject = new PaymentsClient(_apiClient, _apiClient.Configuration);

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