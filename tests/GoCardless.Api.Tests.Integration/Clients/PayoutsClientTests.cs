using GoCardlessApi.Payouts;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class PayoutsClientTests : IntegrationTest
    {
        private IPayoutsClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new PayoutsClient(_configuration);
        }

        [Test]
        public async Task returns_payouts()
        {
            // given
            var options = new GetPayoutsOptions
            {
                PayoutType = PayoutType.Merchant
            };

            // when
            var result = (await _subject.GetPageAsync(options)).Items.ToList();

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
        public async Task maps_paging_properties()
        {
            // given
            var firstPageOptions = new GetPayoutsOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetPayoutsOptions
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1
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
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
        }

        [Test]
        public async Task returns_payout()
        {
            // given
            var payout = (await _subject.GetPageAsync()).Items.First();

            // when
            var result = await _subject.ForIdAsync(payout.Id);
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

        [Test]
        [Category(TestCategory.Paging)]
        public async Task returns_all_payouts_before_specified_payout()
        {
            // given
            var lastId = (await _subject.GetPageAsync()).Items.Last().Id;

            var options = new GetPayoutsOptions
            {
                Before = lastId
            };

            // when
            var results = await _subject
                .PageUsing(options)
                .GetItemsBeforeAsync();

            // then
            Assert.That(results.Count, Is.GreaterThan(1));
            Assert.That(results.Any(x => x.Id == lastId), Is.False);
            Assert.That(results[0].Id, Is.Not.Null.And.Not.EqualTo(results[1].Id));
            Assert.That(results[1].Id, Is.Not.Null.And.Not.EqualTo(results[0].Id));
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task returns_all_payouts_after_specified_payout()
        {
            // given
            var firstId = (await _subject.GetPageAsync()).Items.First().Id;

            var options = new GetPayoutsOptions
            {
                After = firstId
            };

            // when
            var results = await _subject
                .PageUsing(options)
                .GetItemsAfterAsync();

            // then
            Assert.That(results.Count, Is.GreaterThan(1));
            Assert.That(results.Any(x => x.Id == firstId), Is.False);
            Assert.That(results[0].Id, Is.Not.Null.And.Not.EqualTo(results[1].Id));
            Assert.That(results[1].Id, Is.Not.Null.And.Not.EqualTo(results[0].Id));
        }
    }
}