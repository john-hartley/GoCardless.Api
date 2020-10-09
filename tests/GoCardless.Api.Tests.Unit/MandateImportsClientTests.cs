using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
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

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CancelIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new MandateImportsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.CancelAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsCancelMandateImportEndpoint()
        {
            // given
            var subject = new MandateImportsClient(_apiClient);
            var id = "IM12345678";

            // when
            await subject.CancelAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports/IM12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void CreateMandateImportRequestIsNullThrows()
        {
            // given
            var subject = new MandateImportsClient(_apiClient);

            CreateMandateImportRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateMandateImportEndpoint()
        {
            // given
            var subject = new MandateImportsClient(_apiClient);

            var request = new CreateMandateImportRequest();

            // when
            await subject.CreateAsync(request);

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
            var subject = new MandateImportsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualMandateImportEndpoint()
        {
            // given
            var subject = new MandateImportsClient(_apiClient);
            var id = "IM12345678";

            // when
            await subject.ForIdAsync(id);

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
            var subject = new MandateImportsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.SubmitAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsSubmitMandateImportEndpoint()
        {
            // given
            var subject = new MandateImportsClient(_apiClient);
            var id = "IM12345678";

            // when
            await subject.SubmitAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports/IM12345678/actions/submit")
                .WithVerb(HttpMethod.Post);
        }
    }
}