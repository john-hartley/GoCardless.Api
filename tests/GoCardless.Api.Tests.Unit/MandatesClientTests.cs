using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.Mandates;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class MandatesClientTests
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
        public void CancelMandateRequestIsNullThrows()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            CancelMandateRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CancelMandateRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new MandatesClient(_apiClient);

            var request = new CancelMandateRequest
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
        public async Task CallsCancelMandateEndpoint()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            var request = new CancelMandateRequest
            {
                Id = "MD12345678"
            };

            // when
            await subject.CancelAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void CreateMandateRequestIsNullThrows()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            CreateMandateRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateMandateEndpoint()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            var request = new CreateMandateRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await subject.CreateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void IsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new MandatesClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualMandatesEndpoint()
        {
            // given
            var subject = new MandatesClient(_apiClient);
            var id = "MD12345678";

            // when
            await subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetMandatesEndpoint()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetMandatesRequestIsNullThrows()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            GetMandatesRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetMandatesEndpointUsingRequest()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            var request = new GetMandatesRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void ReinstateMandateRequestIsNullThrows()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            ReinstateMandateRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.ReinstateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void ReinstateMandateRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new MandatesClient(_apiClient);

            var request = new ReinstateMandateRequest
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => subject.ReinstateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsReinstateMandateEndpoint()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            var request = new ReinstateMandateRequest
            {
                Id = "MD12345678"
            };

            // when
            await subject.ReinstateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678/actions/reinstate")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void UpdateMandateRequestIsNullThrows()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            UpdateMandateRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateMandateRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new MandatesClient(_apiClient);

            var request = new UpdateMandateRequest
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
        public async Task CallsUpdateMandateEndpoint()
        {
            // given
            var subject = new MandatesClient(_apiClient);

            var request = new UpdateMandateRequest
            {
                Id = "MD12345678"
            };

            // when
            await subject.UpdateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates")
                .WithVerb(HttpMethod.Put);
        }
    }
}