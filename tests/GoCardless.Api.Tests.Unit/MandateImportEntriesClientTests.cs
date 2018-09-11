using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.MandateImportEntries;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class MandateImportEntriesClientTests
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
        public void AddMandateImportEntriesRequestIsNullThrows()
        {
            // given
            var subject = new MandateImportEntriesClient(_clientConfiguration);
            AddMandateImportEntriesRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AddAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsAddMandateImportEntriesEndpoint()
        {
            // given
            var subject = new MandateImportEntriesClient(_clientConfiguration);
            var request = new AddMandateImportEntriesRequest();

            // when
            await subject.AddAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_import_entries")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void AllMandateImportEntriesRequestIsNullThrows()
        {
            // given
            var subject = new MandateImportEntriesClient(_clientConfiguration);
            AllMandateImportEntriesRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void AllMandateImportEntriesRequestMandateImportIsNullOrWhiteSpaceThrows(string mandateImport)
        {
            // given
            var subject = new MandateImportEntriesClient(_clientConfiguration);
            var request = new AllMandateImportEntriesRequest
            {
                MandateImport = mandateImport
            };

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.MandateImport)));
        }

        [Test]
        public async Task CallsAllMandateImportEntriesEndpoint()
        {
            // given
            var subject = new MandateImportEntriesClient(_clientConfiguration);
            var request = new AllMandateImportEntriesRequest
            {
                MandateImport = "IM12345678"
            };

            // when
            await subject.AllAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_import_entries?mandate_import=IM12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}