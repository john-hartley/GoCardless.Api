using Flurl.Http.Testing;
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
        private IMandateImportEntriesClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken"));
            _subject = new MandateImportEntriesClient(apiClient);
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
            TestDelegate test = () => new MandateImportEntriesClient(apiClient);

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
            TestDelegate test = () => new MandateImportEntriesClient(apiClientConfiguration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClientConfiguration)));
        }

        [Test]
        public void AddMandateImportEntryOptionsIsNullThrows()
        {
            // given
            AddMandateImportEntryOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.AddAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsAddMandateImportEntryEndpoint()
        {
            // given
            var options = new AddMandateImportEntryOptions();

            // when
            await _subject.AddAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_import_entries")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void GetMandateImportEntriesOptionsIsNullThrows()
        {
            // given
            GetMandateImportEntriesOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void GetMandateImportEntriesOptionsMandateImportIsNullOrWhiteSpaceThrows(string mandateImport)
        {
            // given
            var options = new GetMandateImportEntriesOptions
            {
                MandateImport = mandateImport
            };

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.MandateImport)));
        }

        [Test]
        public async Task CallsGetMandateImportEntriesEndpoint()
        {
            // given
            var options = new GetMandateImportEntriesOptions
            {
                MandateImport = "IM12345678"
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_import_entries?mandate_import=IM12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}