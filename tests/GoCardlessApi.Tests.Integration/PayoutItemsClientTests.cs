using GoCardlessApi.Core;
using GoCardlessApi.PayoutItems;
using GoCardlessApi.Payouts;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class PayoutItemsClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        private Payout _payout;

        public PayoutItemsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _payout = await _resourceFactory.Payout();
        }

        [Test]
        public async Task ReturnsPayoutItems()
        {
            // given
            var subject = new PayoutItemsClient(_configuration);

            var request = new PayoutItemsRequest
            {
                Payout = _payout.Id
            };

            // when
            var result = await subject.ForPayoutAsync(request);
            var actual = result.PayoutItems.ToList();

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
            var subject = new PayoutItemsClient(_configuration);

            var firstPageRequest = new PayoutItemsRequest
            {
                Limit = 1,
                Payout = _payout.Id
            };

            // when
            var firstPageResult = await subject.ForPayoutAsync(firstPageRequest);

            var secondPageRequest = new PayoutItemsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1,
                Payout = _payout.Id
            };

            var secondPageResult = await subject.ForPayoutAsync(secondPageRequest);

            // then
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(firstPageResult.PayoutItems.Count(), Is.EqualTo(firstPageRequest.Limit));

            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Null);
            Assert.That(secondPageResult.PayoutItems.Count(), Is.EqualTo(secondPageRequest.Limit));
        }
    }
}