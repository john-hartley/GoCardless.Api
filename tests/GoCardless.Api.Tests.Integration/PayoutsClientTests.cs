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
        private IPayoutsClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new PayoutsClient(_configuration);
        }

        [Test]
        public async Task ReturnsPayouts()
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
        public async Task MapsPagingProperties()
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
        public async Task ReturnsIndividualPayout()
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

        [Test, Explicit("Can end up performing lots of calls.")]
        public async Task ReturnsPagesIncludingAndBeforeInitialOptions()
        {
            // given
            var lastId = (await _subject.GetPageAsync()).Items.Last().Id;

            var options = new GetPayoutsOptions
            {
                Before = lastId,
                Limit = 1,
            };

            // when
            var result = await _subject
                .PageFrom(options)
                .AndGetAllBeforeAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }

        [Test, Explicit("Can end up performing lots of calls.")]
        public async Task ReturnsPagesIncludingAndAfterInitialOptions()
        {
            // given
            var options = new GetPayoutsOptions
            {
                Limit = 1,
            };

            // when
            var result = await _subject
                .PageFrom(options)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }

        [Test, Explicit("Can end up performing lots of calls.")]
        public async Task ReturnsPagesIncludingAndAfterInitialOptionsWhenCursorSpecified()
        {
            // given
            var firstId = (await _subject.GetPageAsync()).Items.First().Id;

            var options = new GetPayoutsOptions
            {
                After = firstId,
                Limit = 1,
            };

            // when
            var result = await _subject
                .PageFrom(options)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}