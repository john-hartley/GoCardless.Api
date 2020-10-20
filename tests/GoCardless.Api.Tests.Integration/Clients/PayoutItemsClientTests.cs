using GoCardlessApi.PayoutItems;
using GoCardlessApi.Payouts;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class PayoutItemsClientTests : IntegrationTest
    {
        private IPayoutItemsClient _subject;
        private Payout _payout;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _payout = await _resourceFactory.Payout();
        }

        [SetUp]
        public void Setup()
        {
            _subject = new PayoutItemsClient(_configuration);
        }

        [Test]
        public async Task ReturnsPayoutItems()
        {
            // given
            var options = new GetPayoutItemsOptions
            {
                Payout = _payout.Id
            };

            // when
            var result = await _subject.GetPageAsync(options);
            var actual = result.Items.ToList();

            // then
            Assert.That(actual[0].Amount, Is.Not.Null);
            Assert.That(actual[0].Links, Is.Not.Null);
            Assert.That(actual[0].Links.Payment, Is.Not.Null);
            Assert.That(actual[0].Type, Is.Not.Null);
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task MapsPagingProperties()
        {
            // given
            var firstPageOptions = new GetPayoutItemsOptions
            {
                Limit = 1,
                Payout = _payout.Id
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetPayoutItemsOptions
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1,
                Payout = _payout.Id
            };

            var secondPageResult = await _subject.GetPageAsync(secondPageOptions);

            // then
            Assert.That(firstPageResult.Items.Count(), Is.EqualTo(firstPageOptions.Limit));
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageOptions.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);

            Assert.That(secondPageResult.Items.Count(), Is.EqualTo(secondPageOptions.Limit));
            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageOptions.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Null);
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task ReturnsPagesIncludingAndAfterInitialOptions()
        {
            // given
            var options = new GetPayoutItemsOptions
            {
                Payout = _payout.Id
            };

            // when
            var result = await _subject
                .PageUsing(options)
                .GetItemsAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
        }
    }
}