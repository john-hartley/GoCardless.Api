using Flurl.Http.Testing;
using GoCardlessApi.PayoutItems;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class PayoutItemsClientTests
    {
        private IPayoutItemsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new PayoutItemsClient(configuration);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void throws_when_configuration_not_provided()
        {
            // given
            GoCardlessConfiguration configuration = null;

            // when
            TestDelegate test = () => new PayoutItemsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_get_payout_item_options_not_provided()
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
        public void throws_when_get_payout_item_id_not_provided(string payoutId)
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
        public async Task calls_get_payout_items_endpoint_using_options()
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