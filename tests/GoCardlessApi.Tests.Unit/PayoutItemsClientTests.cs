using Flurl.Http.Testing;
using GoCardlessApi.Core;
using GoCardlessApi.PayoutItems;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit
{
    public class PayoutItemsClientTests
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
        public void PayoutItemRequestIsNullThrows()
        {
            // given
            var subject = new PayoutItemsClient(_clientConfiguration);

            PayoutItemsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.ForPayoutAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void PayoutIdIsNullOrWhiteSpaceThrows(string payoutId)
        {
            // given
            var subject = new PayoutItemsClient(_clientConfiguration);

            var request = new PayoutItemsRequest();

            // when
            AsyncTestDelegate test = () => subject.ForPayoutAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Payout)));
        }

        [Test]
        public async Task CallsAllPayoutsEndpointUsingRequest()
        {
            // given
            var subject = new PayoutItemsClient(_clientConfiguration);

            var request = new PayoutItemsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5,
                Payout = "PO12345678"
            };

            // when
            await subject.ForPayoutAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payout_items?before=before%20test&after=after%20test&limit=5&payout=PO12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}