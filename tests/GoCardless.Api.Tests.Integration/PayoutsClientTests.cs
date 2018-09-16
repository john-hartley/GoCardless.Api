using GoCardless.Api.Payouts;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class PayoutsClientTests : IntegrationTest
    {
        [Test]
        public async Task ReturnsPayouts()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);

            // when
            var result = (await subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Amount, Is.Not.EqualTo(default(int)));
            Assert.That(result[0].ArrivalDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].DeductedFees, Is.Not.Null.And.Not.EqualTo(default(int)));
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Creditor, Is.Not.Null);
            Assert.That(result[0].Links.CreditorBankAccount, Is.Not.Null);
            Assert.That(result[0].PayoutType, Is.Not.Null);
            Assert.That(result[0].Reference, Is.Not.Null);
            Assert.That(result[0].Status, Is.Not.Null);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);

            var firstPageRequest = new GetPayoutsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.GetPageAsync(firstPageRequest);

            var secondPageRequest = new GetPayoutsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1
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
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualPayout()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);
            var payout = (await subject.GetPageAsync()).Items.First();

            // when
            var result = await subject.ForIdAsync(payout.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(payout.Id));
            Assert.That(actual.Amount, Is.Not.Null.And.EqualTo(payout.Amount));
            Assert.That(actual.ArrivalDate, Is.Not.Null.And.EqualTo(payout.ArrivalDate));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(payout.CreatedAt));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(payout.Currency));
            Assert.That(actual.DeductedFees, Is.Not.Null.And.EqualTo(payout.DeductedFees));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Creditor, Is.Not.Null.And.EqualTo(payout.Links.Creditor));
            Assert.That(actual.Links.CreditorBankAccount, Is.Not.Null.And.EqualTo(payout.Links.CreditorBankAccount));
            Assert.That(actual.PayoutType, Is.Not.Null.And.EqualTo(payout.PayoutType));
            Assert.That(actual.Reference, Is.Not.Null.And.EqualTo(payout.Reference));
            Assert.That(actual.Status, Is.Not.Null.And.EqualTo(payout.Status));
        }
    }
}