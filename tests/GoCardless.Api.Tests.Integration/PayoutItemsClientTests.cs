using GoCardless.Api.PayoutItems;
using GoCardless.Api.Payouts;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class PayoutItemsClientTests : IntegrationTest
    {
        private Payout _payout;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _payout = await _resourceFactory.Payout();
        }

        [Test]
        public async Task ReturnsPayoutItems()
        {
            // given
            var subject = new PayoutItemsClient(_clientConfiguration);

            var request = new GetPayoutItemsRequest
            {
                Payout = _payout.Id
            };

            // when
            var result = await subject.GetPageAsync(request);
            var actual = result.Items.ToList();

            // then
            Assert.That(actual[0].Amount, Is.Not.Null);
            Assert.That(actual[0].Links, Is.Not.Null);
            //Assert.That(actual[0].Links.Mandate, Is.Not.Null);
            Assert.That(actual[0].Links.Payment, Is.Not.Null);
            Assert.That(actual[0].Type, Is.Not.Null);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new PayoutItemsClient(_clientConfiguration);

            var firstPageRequest = new GetPayoutItemsRequest
            {
                Limit = 1,
                Payout = _payout.Id
            };

            // when
            var firstPageResult = await subject.GetPageAsync(firstPageRequest);

            var secondPageRequest = new GetPayoutItemsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1,
                Payout = _payout.Id
            };

            var secondPageResult = await subject.GetPageAsync(secondPageRequest);

            // then
            Assert.That(firstPageResult.Items.Count(), Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);

            Assert.That(secondPageResult.Items.Count(), Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Null);
        }

        [Test]
        public async Task ReturnsPagesIncludingAndAfterInitialRequest()
        {
            // given
            var subject = new PayoutItemsClient(_clientConfiguration);

            var initialRequest = new GetPayoutItemsRequest
            {
                Limit = 1,
                Payout = _payout.Id
            };

            // when
            var result = await subject
                .BuildPager()
                .StartFrom(initialRequest)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
        }
    }
}