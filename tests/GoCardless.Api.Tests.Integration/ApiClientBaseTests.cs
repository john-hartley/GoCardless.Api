using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Exceptions;
using GoCardless.Api.Creditors;
using GoCardless.Api.CustomerBankAccounts;
using GoCardless.Api.Customers;
using GoCardless.Api.Mandates;
using GoCardless.Api.Payments;
using GoCardless.Api.Refunds;
using GoCardless.Api.Subscriptions;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class ApiClientBaseTests : IntegrationTest
    {
        private Creditor _creditor;
        private CustomerBankAccount _customerBankAccount;
        private Mandate _mandate;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            _customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(_creditor, customer, _customerBankAccount);
        }

        [Test]
        public void ValidationFailsForRequestThrows()
        {
            // given
            var request = new CreateCustomerRequest
            {
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name",
                CountryCode = "DK",
                Email = "email@example.com",
                FamilyName = "Family Name",
                GivenName = "Given Name",
                Language = "incorrect language", // This triggers the error.
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                DanishIdentityNumber = "2205506218",
                SwedishIdentityNumber = "5302256218",
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
            };

            var subject = new CustomersClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ValidationFailedException>(test);
            Assert.That(ex.Code, Is.EqualTo((int)HttpStatusCode.UnprocessableEntity));
            Assert.That(ex.DocumentationUrl, Is.Not.Null);
            Assert.That(ex.Errors?.Any(), Is.True);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RawResponse, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RequestId, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task CustomerAlreadyExistsThrows()
        {
            // given
            var request = new CreateCustomerRequest
            {
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name",
                CountryCode = "DK",
                Email = "email@example.com",
                FamilyName = "Family Name",
                GivenName = "Given Name",
                IdempotencyKey = Guid.NewGuid().ToString(),
                Language = "da",
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                DanishIdentityNumber = "2205506218",
                SwedishIdentityNumber = "5302256218",
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
            };

            var subject = new CustomersClient(_clientConfiguration);

            // when
            var result = await subject.CreateAsync(request);
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ResourceAlreadyExistsException>(test);
            Assert.That(ex.Code, Is.EqualTo((int)HttpStatusCode.Conflict));
            Assert.That(ex.DocumentationUrl, Is.Not.Null);
            Assert.That(ex.Errors?.Any(), Is.True);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RawResponse, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RequestId, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.ResourceId, Is.EqualTo(result.Item.Id));
        }

        [Test, NonParallelizable]
        public async Task MandateAlreadyExistsThrows()
        {
            // given
            var request = new CreateMandateRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString(),
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Scheme = "bacs",
                Links = new CreateMandateLinks
                {
                    Creditor = _creditor.Id,
                    CustomerBankAccount = _customerBankAccount.Id
                }
            };

            var subject = new MandatesClient(_clientConfiguration);

            // when
            var result = await subject.CreateAsync(request);
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ResourceAlreadyExistsException>(test);
            Assert.That(ex.Code, Is.EqualTo((int)HttpStatusCode.Conflict));
            Assert.That(ex.DocumentationUrl, Is.Not.Null);
            Assert.That(ex.Errors?.Any(), Is.True);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RawResponse, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RequestId, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.ResourceId, Is.EqualTo(result.Item.Id));
        }

        [Test]
        public async Task PaymentAlreadyExistsThrows()
        {
            // given
            var request = new CreatePaymentRequest
            {
                Amount = 500,
                ChargeDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Description = "Sandbox Payment",
                Currency = "GBP",
                IdempotencyKey = Guid.NewGuid().ToString(),
                Links = new CreatePaymentLinks { Mandate = _mandate.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                }
            };

            var subject = new PaymentsClient(_clientConfiguration);

            // when
            var result = await subject.CreateAsync(request);
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ResourceAlreadyExistsException>(test);
            Assert.That(ex.Code, Is.EqualTo((int)HttpStatusCode.Conflict));
            Assert.That(ex.DocumentationUrl, Is.Not.Null);
            Assert.That(ex.Errors?.Any(), Is.True);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RawResponse, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RequestId, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.ResourceId, Is.EqualTo(result.Item.Id));
        }

        [Test]
        public async Task CreateRefundRequestIsInvalidThrows()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var request = new CreateRefundRequest
            {
                Amount = 100,
                IdempotencyKey = Guid.NewGuid().ToString(),
                Links = new CreateRefundLinks { Payment = payment.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                TotalAmountConfirmation = 100
            };

            var subject = new RefundsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<InvalidStateException>(test);
            Assert.That(ex.Code, Is.EqualTo((int)HttpStatusCode.UnprocessableEntity));
            Assert.That(ex.DocumentationUrl, Is.Not.Null);
            Assert.That(ex.Errors?.Any(), Is.True);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RawResponse, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RequestId, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task SubscriptionAlreadyExistsThrows()
        {
            // given
            var request = new CreateSubscriptionRequest
            {
                Amount = 123,
                Count = 5,
                Currency = "GBP",
                IdempotencyKey = Guid.NewGuid().ToString(),
                Interval = 1,
                IntervalUnit = "weekly",
                Links = new SubscriptionLinks
                {
                    Mandate = _mandate.Id
                },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Name = "Test subscription",
                PaymentReference = "PR123456",
                StartDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd")
            };

            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            var result = await subject.CreateAsync(request);
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ResourceAlreadyExistsException>(test);
            Assert.That(ex.Code, Is.EqualTo((int)HttpStatusCode.Conflict));
            Assert.That(ex.DocumentationUrl, Is.Not.Null);
            Assert.That(ex.Errors?.Any(), Is.True);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RawResponse, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RequestId, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.ResourceId, Is.EqualTo(result.Item.Id));
        }

        [Test]
        public void ApiUsageIsInvalidInvalidThrows()
        {
            // given
            var request = new CreateSubscriptionRequest
            {
                Amount = 123,
                Currency = "GBP",
                IntervalUnit = "weekly",
                Count = 5,
                Interval = 1,
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Name = "Test subscription",
                StartDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Links = new SubscriptionLinks
                {
                    Mandate = _mandate.Id
                }
            };

            var subject = new SubscriptionsClient(ClientConfiguration.ForSandbox("invalid token"));

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<InvalidApiUsageException>(test);
            Assert.That(ex.Code, Is.EqualTo((int)HttpStatusCode.Unauthorized));
            Assert.That(ex.DocumentationUrl, Is.Not.Null);
            Assert.That(ex.Errors?.Any(), Is.True);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RawResponse, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RequestId, Is.Not.Null.And.Not.Empty);
        }
    }
}