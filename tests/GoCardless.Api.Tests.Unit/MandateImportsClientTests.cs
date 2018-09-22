using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.MandateImports;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class MandateImportsClientTests
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

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CancelIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new MandateImportsClient(_clientConfiguration);

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
            var subject = new MandateImportsClient(_clientConfiguration);
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
            var subject = new MandateImportsClient(_clientConfiguration);

            CreateMandateImportRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsCreateMandateImportEndpoint()
        {
            // given
            var subject = new MandateImportsClient(_clientConfiguration);

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
            var subject = new MandateImportsClient(_clientConfiguration);

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
            var subject = new MandateImportsClient(_clientConfiguration);
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
            var subject = new MandateImportsClient(_clientConfiguration);

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
            var subject = new MandateImportsClient(_clientConfiguration);
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