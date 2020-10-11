using Flurl.Http.Testing;
using GoCardless.Api.BankDetailsLookups;
using GoCardless.Api.Core.Http;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class BankDetailsLookupsClientTests
    {
        private IBankDetailsLookupsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken", false));
            _subject = new BankDetailsLookupsClient(apiClient);
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
            TestDelegate test = () => new BankDetailsLookupsClient(apiClient);

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
            TestDelegate test = () => new BankDetailsLookupsClient(apiClientConfiguration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClientConfiguration)));
        }

        [Test]
        public void BankDetailsLookupOptionsIsNullThrows()
        {
            // given
            BankDetailsLookupOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.LookupAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task BankDetailsLookupEndpoint()
        {
            // given
            var options = new BankDetailsLookupOptions();

            // when
            await _subject.LookupAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/bank_details_lookups")
                .WithVerb(HttpMethod.Post);
        }
    }
}