using GoCardlessApi.Core;
using GoCardlessApi.Mandates;
using GoCardlessApi.Refunds;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class RefundsClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        private Mandate _mandate;

        public RefundsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(creditor, customer, customerBankAccount);
        }

        [Test, Explicit("Need to use scenario simulators to activate the mandate, and pay out the created payment, before continuing.")]
        public async Task CreatesRefund()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var request = new CreateRefundRequest
            {
                Amount = 100,
                Links = new RefundLinks { Payment = payment.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Reference = "RF123456",
                TotalAmountConfirmation = 100
            };

            var subject = new RefundsClient(_configuration);

            // when
            var result = await subject.CreateAsync(request);
            var actual = result.Refund;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Amount, Is.EqualTo(request.Amount));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Currency, Is.EqualTo(payment.Currency));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Payment, Is.EqualTo(request.Links.Payment));
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(actual.Reference, Is.EqualTo(request.Reference));
        }
    }
}