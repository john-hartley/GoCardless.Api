using GoCardless.Api.Creditors;
using GoCardless.Api.Mandates;
using GoCardless.Api.Payments;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class PaymentsClientTests : IntegrationTest
    {
        private Creditor _creditor;
        private Mandate _mandate;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(_creditor, customer, customerBankAccount);
        }

        [Test]
        public async Task CreatesAndCancelsPayment()
        {
            // given
            var createRequest = new CreatePaymentRequest
            {
                Amount = 500,
                //AppFee = 50,
                ChargeDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Description = "Sandbox Payment",
                Currency = "GBP",
                Links = new CreatePaymentLinks { Mandate = _mandate.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Reference = "REF123456"
            };

            var subject = new PaymentsClient(_clientConfiguration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var cancelRequest = new CancelPaymentRequest
            {
                Id = creationResult.Item.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            var cancellationResult = await subject.CancelAsync(cancelRequest);

            // then
            Assert.That(creationResult.Item.Id, Is.Not.Null);
            Assert.That(creationResult.Item.Amount, Is.EqualTo(createRequest.Amount));
            Assert.That(creationResult.Item.AmountRefunded, Is.Not.Null);
            //Assert.That(creationResult.Item.AppFee, Is.EqualTo(createRequest.AppFee));
            Assert.That(creationResult.Item.ChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(creationResult.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(creationResult.Item.Currency, Is.EqualTo(createRequest.Currency));
            Assert.That(creationResult.Item.Description, Is.EqualTo(createRequest.Description));
            Assert.That(creationResult.Item.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(creationResult.Item.Links.Mandate, Is.EqualTo(_mandate.Id));
            Assert.That(creationResult.Item.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.Item.Reference, Is.EqualTo(createRequest.Reference));
            Assert.That(creationResult.Item.Status, Is.Not.Null.And.Not.EqualTo("cancelled"));

            Assert.That(cancellationResult.Item.Status, Is.EqualTo("cancelled"));
        }

        [Test]
        public async Task ReturnsPayments()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            // when
            var result = (await subject.AllAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Amount, Is.Not.EqualTo(default(int)));
            Assert.That(result[0].AmountRefunded, Is.EqualTo(0));
            //Assert.That(result[0].AppFee, Is.Not.Null);
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
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);

            var firstPageRequest = new AllPaymentsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.AllAsync(firstPageRequest);

            var secondPageRequest = new AllPaymentsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await subject.AllAsync(secondPageRequest);

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
        public async Task ReturnsIndividualPayment()
        {
            // given
            var subject = new PaymentsClient(_clientConfiguration);
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            // when
            var result = await subject.ForIdAsync(payment.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Amount, Is.EqualTo(payment.Amount));
            Assert.That(actual.AmountRefunded, Is.EqualTo(0));
            //Assert.That(actual.AppFee, Is.Not.Null);
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
        public async Task UpdatesPaymentPreservingMetadata()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var request = new UpdatePaymentRequest
            {
                Id = payment.Id
            };

            var subject = new PaymentsClient(_clientConfiguration);

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(payment.Id));
            Assert.That(actual.Metadata, Is.EqualTo(payment.Metadata));
        }

        [Test]
        public async Task UpdatesPaymentReplacingMetadata()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var request = new UpdatePaymentRequest
            {
                Id = payment.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            var subject = new PaymentsClient(_clientConfiguration);

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(payment.Id));
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }

        [Test, Explicit("Need to use scenario simulators to activate the mandate, and fail the created payment, before continuing.")]
        public async Task RetriesPayment()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var request = new RetryPaymentRequest
            {
                Id = payment.Id
            };

            var subject = new PaymentsClient(_clientConfiguration);

            // when
            var result = await subject.RetryAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(payment.Id));
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }
    }
}