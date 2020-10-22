using GoCardlessApi.Creditors;
using GoCardlessApi.Mandates;
using GoCardlessApi.Payments;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class PaymentsClientTests : IntegrationTest
    {
        private IPaymentsClient _subject;
        private Creditor _creditor;
        private Mandate _mandate;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(_creditor, customerBankAccount);
        }

        [SetUp]
        public void Setup()
        {
            _subject = new PaymentsClient(_configuration);
        }

        [Test]
        public async Task creates_and_cancels_conflicting_payment()
        {
            // given
            var createOptions = new CreatePaymentOptions
            {
                Amount = 500,
                ChargeDate = DateTime.Now.AddMonths(1),
                Description = "Sandbox Payment",
                Currency = "GBP",
                Links = new CreatePaymentLinks { Mandate = _mandate.Id },
                Metadata = Metadata.Initial,
                Reference = "REF123456"
            };

            // when
            await _subject.CreateAsync(createOptions);
            var createResult = await _subject.CreateAsync(createOptions);

            var cancelOptions = new CancelPaymentOptions
            {
                Id = createResult.Item.Id,
                Metadata = Metadata.Updated
            };

            var cancelResult = await _subject.CancelAsync(cancelOptions);

            // then
            Assert.That(createResult.Item.Id, Is.Not.Null);
            Assert.That(createResult.Item.Amount, Is.EqualTo(createOptions.Amount));
            Assert.That(createResult.Item.AmountRefunded, Is.Not.Null);
            Assert.That(createResult.Item.ChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(createResult.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(createResult.Item.Currency, Is.EqualTo(createOptions.Currency));
            Assert.That(createResult.Item.Description, Is.EqualTo(createOptions.Description));
            Assert.That(createResult.Item.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(createResult.Item.Links.Mandate, Is.EqualTo(_mandate.Id));
            Assert.That(createResult.Item.Metadata, Is.EqualTo(createOptions.Metadata));
            Assert.That(createResult.Item.Reference, Is.EqualTo(createOptions.Reference));
            Assert.That(createResult.Item.Status, Is.Not.Null.And.Not.EqualTo(PaymentStatus.Cancelled));

            Assert.That(cancelResult.Item.Status, Is.EqualTo(PaymentStatus.Cancelled));
        }

        [Test, Explicit("Needs a merchant account to be setup, an OAuth access token to have been exchanged, and a mandate setup via a redirect flow.")]
        [Category(TestCategory.NeedsMerchantAccount)]
        public async Task creates_and_cancels_payment_for_merchant()
        {
            var accessToken = Environment.GetEnvironmentVariable("GoCardlessMerchantAccessToken");
            var configuration = GoCardlessConfiguration.ForSandbox(accessToken, false);
            var resourceFactory = new ResourceFactory(configuration);

            var creditor = await resourceFactory.Creditor();
            var mandatesClient = new MandatesClient(configuration);
            var mandate = (await mandatesClient.GetPageAsync()).Items.First();

            // given
            var createOptions = new CreatePaymentOptions
            {
                Amount = 500,
                AppFee = 12,
                ChargeDate = DateTime.Now.AddMonths(1),
                Description = "Sandbox Payment",
                Currency = "GBP",
                Links = new CreatePaymentLinks { Mandate = mandate.Id },
                Metadata = Metadata.Initial
            };

            // when
            _subject = new PaymentsClient(configuration);
            var createResult = await _subject.CreateAsync(createOptions);

            var cancelOptions = new CancelPaymentOptions
            {
                Id = createResult.Item.Id,
                Metadata = Metadata.Updated
            };

            var cancelResult = await _subject.CancelAsync(cancelOptions);

            // then
            Assert.That(createResult.Item.Id, Is.Not.Null);
            Assert.That(createResult.Item.Amount, Is.EqualTo(createOptions.Amount));
            Assert.That(createResult.Item.AmountRefunded, Is.Not.Null);
            Assert.That(createResult.Item.ChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(createResult.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(createResult.Item.Currency, Is.EqualTo(createOptions.Currency));
            Assert.That(createResult.Item.Description, Is.EqualTo(createOptions.Description));
            Assert.That(createResult.Item.Links.Creditor, Is.EqualTo(creditor.Id));
            Assert.That(createResult.Item.Links.Mandate, Is.EqualTo(mandate.Id));
            Assert.That(createResult.Item.Metadata, Is.EqualTo(createOptions.Metadata));
            Assert.That(createResult.Item.Reference, Is.EqualTo(createOptions.Reference));
            Assert.That(createResult.Item.Status, Is.Not.Null.And.Not.EqualTo(PaymentStatus.Cancelled));

            Assert.That(cancelResult.Item.Status, Is.EqualTo(PaymentStatus.Cancelled));
        }

        [Test]
        public async Task returns_payments()
        {
            // given
            // when
            var result = (await _subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Amount, Is.Not.EqualTo(default(int)));
            Assert.That(result[0].AmountRefunded, Is.EqualTo(0));
            Assert.That(result[0].ChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Description, Is.Not.Null);
            Assert.That(result[0].Links.Creditor, Is.Not.Null);
            Assert.That(result[0].Links.Mandate, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].Reference, Is.Not.Null);
            Assert.That(result[0].Status, Is.Not.Null);
        }

        [Test]
        public async Task maps_paging_properties()
        {
            // given
            var firstPageOptions = new GetPaymentsOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetPaymentsOptions
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
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
        [Category(TestCategory.Paging)]
        public async Task returns_payments_when_limit_is_higher_than_api_maximum()
        {
            // given
            var options = new GetPaymentsOptions
            {
                Limit = 1001
            };

            // when
            var afterResults = (await _subject
                .PageUsing(options)
                .GetItemsAfterAsync())
                .Select(x => new { x.Id, x.CreatedAt })
                .ToList();

            options.Before = afterResults.Last().Id;

            afterResults = afterResults
                .Take(1000)
                .OrderBy(x => x.CreatedAt)
                .ToList();

            var beforeResults = (await _subject
                .PageUsing(options)
                .GetItemsBeforeAsync())
                .OrderBy(x => x.CreatedAt)
                .Select(x => new { x.Id, x.CreatedAt })
                .ToList();

            // then
            Assert.That(afterResults.SequenceEqual(beforeResults), Is.True);
        }

        [Test]
        public async Task returns_payment()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            // when
            var result = await _subject.ForIdAsync(payment.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Amount, Is.EqualTo(payment.Amount));
            Assert.That(actual.AmountRefunded, Is.EqualTo(0));
            Assert.That(actual.ChargeDate, Is.EqualTo(payment.ChargeDate));
            Assert.That(actual.Currency, Is.EqualTo(payment.Currency));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Description, Is.EqualTo(payment.Description));
            Assert.That(actual.Links.Creditor, Is.EqualTo(payment.Links.Creditor));
            Assert.That(actual.Links.Mandate, Is.EqualTo(payment.Links.Mandate));
            Assert.That(actual.Metadata, Is.EqualTo(payment.Metadata));
            Assert.That(actual.Reference, Is.EqualTo(payment.Reference));
            Assert.That(actual.Status, Is.EqualTo(payment.Status));
        }

        [Test]
        public async Task updates_payment_preserving_metadata()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var options = new UpdatePaymentOptions
            {
                Id = payment.Id
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(payment.Id));
            Assert.That(actual.Metadata, Is.EqualTo(payment.Metadata));
        }

        [Test]
        public async Task updates_payment_replacing_metadata()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var options = new UpdatePaymentOptions
            {
                Id = payment.Id,
                Metadata = Metadata.Updated
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(payment.Id));
            Assert.That(actual.Metadata, Is.EqualTo(options.Metadata));
        }

        [Test, Explicit("Need to use scenario simulators to activate the mandate, and fail the created payment, before continuing.")]
        [Category(TestCategory.NeedsManualIntervention)]
        public async Task retries_failed_payment()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var options = new RetryPaymentOptions
            {
                Id = payment.Id,
                Metadata = Metadata.Initial
            };

            // when
            var result = await _subject.RetryAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(payment.Id));
            Assert.That(actual.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(actual.Status, Is.EqualTo(PaymentStatus.PendingSubmission));
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task pages_through_payments()
        {
            // given
            var firstId = (await _subject.GetPageAsync()).Items.First().Id;

            var options = new GetPaymentsOptions
            {
                After = firstId,
                CreatedGreaterThan = new DateTimeOffset(DateTime.Now.AddDays(-1))
            };

            // when
            var result = await _subject
                .PageUsing(options)
                .GetItemsAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}