using Flurl.Http.Testing;
using GoCardless.Api.Core.Http;
using GoCardless.Api.MandateImports;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class MandateImportsClientTests
    {
        private IMandateImportsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken", false));
            _subject = new MandateImportsClient(apiClient);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void ApiClientIsNullThrows()
        {
            // given
            IApiClient apiClient = null;

            // when
            TestDelegate test = () => new MandateImportsClient(apiClient);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClient)));
        }

        [Test]
        public void ApiClientConfigurationIsNullThrows()
        {
            // given
            ApiClientConfiguration apiClientConfiguration = null;

            // when
            TestDelegate test = () => new MandateImportsClient(apiClientConfiguration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClientConfiguration)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CancelIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            // when
            AsyncTestDelegate test = () => _subject.CancelAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsCancelMandateImportEndpoint()
        {
            // given
            var id = "IM12345678";

            // when
            await _subject.CancelAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports/IM12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void CreateMandateImportOptionsIsNullThrows()
        {
            // given
            CreateMandateImportOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateMandateImportEndpoint()
        {
            // given
            var options = new CreateMandateImportOptions();

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            // when
            AsyncTestDelegate test = () => _subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualMandateImportEndpoint()
        {
            // given
            var id = "IM12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports/IM12345678")
                .WithVerb(HttpMethod.Get);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void SubmitIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            // when
            AsyncTestDelegate test = () => _subject.SubmitAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsSubmitMandateImportEndpoint()
        {
            // given
            var id = "IM12345678";

            // when
            await _subject.SubmitAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports/IM12345678/actions/submit")
                .WithVerb(HttpMethod.Post);
        }
    }
}