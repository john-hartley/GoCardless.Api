using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.PayoutItems;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class PayoutItemsClientTests
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
        public void GetPayoutItemRequestIsNullThrows()
        {
            // given
            var subject = new PayoutItemsClient(_apiClient, _apiClient.Configuration);

            GetPayoutItemsRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void PayoutIdIsNullOrWhiteSpaceThrows(string payoutId)
        {
            // given
            var subject = new PayoutItemsClient(_apiClient, _apiClient.Configuration);

            var request = new GetPayoutItemsRequest
            {
                Payout = payoutId
            };

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Payout)));
        }

        [Test]
        public async Task CallsGetPayoutItemsEndpointUsingRequest()
        {
            // given
            var subject = new PayoutItemsClient(_apiClient, _apiClient.Configuration);

            var request = new GetPayoutItemsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5,
                Payout = "PO12345678"
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payout_items?before=before%20test&after=after%20test&limit=5&payout=PO12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}