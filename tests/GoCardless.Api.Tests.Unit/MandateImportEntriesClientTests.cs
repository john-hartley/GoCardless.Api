using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.MandateImportEntries;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class MandateImportEntriesClientTests
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
        public void AddMandateImportEntryRequestIsNullThrows()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient);
            AddMandateImportEntryRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.AddAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsAddMandateImportEntryEndpoint()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient);
            var request = new AddMandateImportEntryRequest();

            // when
            await subject.AddAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_import_entries")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void GetMandateImportEntriesRequestIsNullThrows()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient);
            GetMandateImportEntriesRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void GetMandateImportEntriesRequestMandateImportIsNullOrWhiteSpaceThrows(string mandateImport)
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient);
            var request = new GetMandateImportEntriesRequest
            {
                MandateImport = mandateImport
            };

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.MandateImport)));
        }

        [Test]
        public async Task CallsGetMandateImportEntriesEndpoint()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient);
            var request = new GetMandateImportEntriesRequest
            {
                MandateImport = "IM12345678"
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_import_entries?mandate_import=IM12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}