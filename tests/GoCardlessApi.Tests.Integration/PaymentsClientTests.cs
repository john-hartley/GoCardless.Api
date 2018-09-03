using GoCardlessApi.Core;
using GoCardlessApi.Creditors;
using GoCardlessApi.Mandates;
using GoCardlessApi.Payments;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class PaymentsClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        private Creditor _creditor;
        private Mandate _mandate;

        public PaymentsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

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

            var subject = new PaymentsClient(_configuration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var cancelRequest = new CancelPaymentRequest
            {
                Id = creationResult.Payment.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            var cancellationResult = await subject.CancelAsync(cancelRequest);

            // then
            Assert.That(creationResult.Payment.Id, Is.Not.Null);
            Assert.That(creationResult.Payment.Amount, Is.EqualTo(createRequest.Amount));
            Assert.That(creationResult.Payment.AmountRefunded, Is.Not.Null);
            //Assert.That(creationResult.Payment.AppFee, Is.EqualTo(createRequest.AppFee));
            Assert.That(creationResult.Payment.ChargeDate.ToString("yyyy-MM-dd"), Is.EqualTo(createRequest.ChargeDate));
            Assert.That(creationResult.Payment.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(creationResult.Payment.Currency, Is.EqualTo(createRequest.Currency));
            Assert.That(creationResult.Payment.Description, Is.EqualTo(createRequest.Description));
            Assert.That(creationResult.Payment.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(creationResult.Payment.Links.Mandate, Is.EqualTo(_mandate.Id));
            Assert.That(creationResult.Payment.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.Payment.Reference, Is.EqualTo(createRequest.Reference));
            Assert.That(creationResult.Payment.Status, Is.Not.Null.And.Not.EqualTo("cancelled"));

            Assert.That(cancellationResult.Payment.Status, Is.EqualTo("cancelled"));
        }

        [Test]
        public async Task ReturnsPayments()
        {
            // given
            var subject = new PaymentsClient(_configuration);

            // when
            var result = (await subject.AllAsync()).Payments.ToList();

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
        public async Task ReturnsIndividualPayment()
        {
            // given
            var subject = new PaymentsClient(_configuration);
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            // when
            var result = await subject.ForIdAsync(payment.Id);
            var actual = result.Payment;

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

            var subject = new PaymentsClient(_configuration);

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Payment;

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

            var subject = new PaymentsClient(_configuration);

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Payment;

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
                Id = payment.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            var subject = new PaymentsClient(_configuration);

            // when
            var result = await subject.RetryAsync(request);
            var actual = result.Payment;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(payment.Id));
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }
    }
}