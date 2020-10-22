using GoCardlessApi.Mandates;
using GoCardlessApi.Refunds;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class RefundsClientTests : IntegrationTest
    {
        private IRefundsClient _subject;
        private Mandate _mandate;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(creditor, customerBankAccount);
        }

        [SetUp]
        public void Setup()
        {
            _subject = new RefundsClient(_configuration);
        }

        [Test, Explicit("Use scenario simulators to activate the mandate, and pay out the created payment, before continuing.")]
        [Category(TestCategory.NeedsManualIntervention)]
        public async Task creates_refund()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var options = new CreateRefundOptions
            {
                Amount = 100,
                Links = new CreateRefundLinks { Payment = payment.Id },
                Metadata = Metadata.Initial,
                Reference = "RF123456",
                TotalAmountConfirmation = 100
            };

            // when
            var result = await _subject.CreateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Amount, Is.EqualTo(options.Amount));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Currency, Is.EqualTo(payment.Currency));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Mandate, Is.EqualTo(_mandate.Id));
            Assert.That(actual.Links.Payment, Is.EqualTo(options.Links.Payment));
            Assert.That(actual.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(actual.Reference, Is.EqualTo(options.Reference));
        }

        [Test, NonParallelizable]
        public async Task returns_refunds()
        {
            // given
            // when
            var result = (await _subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Amount, Is.Not.EqualTo(default(int)));
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Mandate, Is.Not.Null);
            Assert.That(result[0].Links.Payment, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].Reference, Is.Not.Null);
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task maps_paging_properties()
        {
            // given
            var firstPageOptions = new GetRefundsOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetRefundsOptions
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

        [Test, NonParallelizable]
        public async Task returns_refund()
        {
            // given
            var refund = (await _subject.GetPageAsync()).Items.First();

            // when
            var result = await _subject.ForIdAsync(refund.Id);
            var actual = result.Item;

            // then
            Validate(actual, refund);
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(refund.Metadata));
        }

        [Test, NonParallelizable]
        public async Task updates_refund_preserving_metadata()
        {
            // given
            var refund = (await _subject.GetPageAsync()).Items.First();

            var options = new UpdateRefundOptions
            {
                Id = refund.Id
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Validate(actual, refund);
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(refund.Metadata));
        }

        [Test, NonParallelizable]
        public async Task updates_refund_replacing_metadata()
        {
            // given
            var refund = (await _subject.GetPageAsync()).Items.First();
            var now = DateTime.Now.ToString("yyyyMMdd");

            var options = new UpdateRefundOptions
            {
                Id = refund.Id,
                Metadata = new Dictionary<string, string>
                {
                    [$"Key-1-{now}"] = $"Value-1-{now}",
                    [$"Key-2-{now}"] = $"Value-2-{now}",
                    [$"Key-3-{now}"] = $"Value-3-{now}",
                },
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Validate(actual, refund);
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(options.Metadata));
        }

        [Test]
        public async Task pages_through_refunds()
        {
            // given
            var options = new GetRefundsOptions();

            // when
            var result = await _subject
                .PageUsing(options)
                .GetItemsAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }

        private static void Validate(Refund actual, Refund expected)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(expected.Id));
            Assert.That(actual.Amount, Is.Not.Null.And.EqualTo(expected.Amount));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(expected.Currency));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(expected.CreatedAt));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Mandate, Is.Not.Null.And.EqualTo(expected.Links.Mandate));
            Assert.That(actual.Links.Payment, Is.Not.Null.And.EqualTo(expected.Links.Payment));
            Assert.That(actual.Reference, Is.Not.Null.And.EqualTo(expected.Reference));
        }
    }
}