using Flurl.Http.Testing;
using GoCardlessApi.Core;
using GoCardlessApi.BankDetailsLookups;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit
{
    public class BankDetailsLookupsClientTests
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
        public void BankDetailsLookupRequestIsNullThrows()
        {
            // given
            var subject = new BankDetailsLookupsClient(_clientConfiguration);

            BankDetailsLookupRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.LookupAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task BankDetailsLookupEndpoint()
        {
            // given
            var subject = new BankDetailsLookupsClient(_clientConfiguration);

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