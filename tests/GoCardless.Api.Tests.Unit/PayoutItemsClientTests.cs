using Flurl.Http.Testing;
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
        private IPayoutItemsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken"));
            _subject = new PayoutItemsClient(apiClient);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void GetPayoutItemOptionsIsNullThrows()
        {
            // given
            GetPayoutItemsOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

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
            var options = new GetPayoutItemsOptions
            {
                Payout = payoutId
            };

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Payout)));
        }

        [Test]
        public async Task CallsGetPayoutItemsEndpointUsingOptions()
        {
            // given
            var options = new GetPayoutItemsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5,
                Payout = "PO12345678"
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/payout_items?before=before%20test&after=after%20test&limit=5&payout=PO12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}