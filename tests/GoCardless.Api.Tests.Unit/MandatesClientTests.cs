using Flurl.Http.Testing;
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
        private IMandatesClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = ApiClientConfiguration.ForLive("accesstoken", false);
            _subject = new MandatesClient(configuration);
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
            TestDelegate test = () => new MandatesClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void CancelMandateOptionsIsNullThrows()
        {
            // given
            CancelMandateOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CancelMandateOptionsIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var options = new CancelMandateOptions
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
        public async Task CallsCancelMandateEndpoint()
        {
            // given
            var options = new CancelMandateOptions
            {
                Id = "MD12345678"
            };

            // when
            await _subject.CancelAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void CreateMandateOptionsIsNullThrows()
        {
            // given
            CreateMandateOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateMandateEndpoint()
        {
            // given
            var options = new CreateMandateOptions
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await _subject.CreateAsync(options);

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
            // when
            AsyncTestDelegate test = () => _subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualMandatesEndpoint()
        {
            // given
            var id = "MD12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetMandatesEndpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetMandatesOptionsIsNullThrows()
        {
            // given
            GetMandatesOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetMandatesEndpointUsingOptions()
        {
            // given
            var options = new GetMandatesOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void ReinstateMandateOptionsIsNullThrows()
        {
            // given
            ReinstateMandateOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.ReinstateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void ReinstateMandateOptionsIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var options = new ReinstateMandateOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.ReinstateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task CallsReinstateMandateEndpoint()
        {
            // given
            var options = new ReinstateMandateOptions
            {
                Id = "MD12345678"
            };

            // when
            await _subject.ReinstateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678/actions/reinstate")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void UpdateMandateOptionsIsNullThrows()
        {
            // given
            UpdateMandateOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateMandateOptionsIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var options = new UpdateMandateOptions
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
        public async Task CallsUpdateMandateEndpoint()
        {
            // given
            var options = new UpdateMandateOptions
            {
                Id = "MD12345678"
            };

            // when
            await _subject.UpdateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates")
                .WithVerb(HttpMethod.Put);
        }
    }
}