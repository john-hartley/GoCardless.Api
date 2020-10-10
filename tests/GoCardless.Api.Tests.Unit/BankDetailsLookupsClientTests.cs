using Flurl.Http.Testing;
using GoCardless.Api.BankDetailsLookups;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class BankDetailsLookupsClientTests
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
        public void BankDetailsLookupRequestIsNullThrows()
        {
            // given
            var subject = new BankDetailsLookupsClient(_apiClient);

            BankDetailsLookupRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.LookupAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task BankDetailsLookupEndpoint()
        {
            // given
            var subject = new BankDetailsLookupsClient(_apiClient);

            var request = new BankDetailsLookupRequest();

            // when
            await subject.LookupAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/bank_details_lookups")
                .WithVerb(HttpMethod.Post);
        }
    }
}