using GoCardless.Api.Core.Exceptions;
using GoCardless.Api.Core.Http;
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
using Flurl.Http;
using System.Security.Cryptography.X509Certificates;

namespace GoCardless.Api.Tests.Integration.Core.Http
{
    public class ApiClientTests : IntegrationTest
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
            _mandate = await _resourceFactory.CreateMandateFor(_creditor, _customerBankAccount);
        }

        [Test]
        public void ValidationFailsForOptionsThrows()
        {
            // given
            var options = new CreateCustomerOptions
            {
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name",
                CountryCode = "DK",
                DanishIdentityNumber = "2205506218",
                Email = "email@example.com",
                FamilyName = "Family Name",
                GivenName = "Given Name",
                Language = "incorrect language", // This triggers the error.
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                SwedishIdentityNumber = "5302256218",
            };

            var subject = new CustomersClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

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
        public async Task CreateRefundOptionsIsInvalidThrows()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var options = new CreateRefundOptions
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

            var subject = new RefundsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

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
        public void ApiUsageIsInvalidThrows()
        {
            // given
            var options = new CreateSubscriptionOptions
            {
                Amount = 123,
                Currency = "GBP",
                IntervalUnit = IntervalUnit.Weekly,
                Count = 5,
                Interval = 1,
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Name = "Test subscription",
                StartDate = DateTime.Now.AddMonths(1),
                Links = new SubscriptionLinks
                {
                    Mandate = _mandate.Id
                }
            };

            var apiClient = new ApiClient(ApiClientConfiguration.ForSandbox("invalid token", false));
            var subject = new SubscriptionsClient(apiClient);

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<InvalidApiUsageException>(test);
            Assert.That(ex.Code, Is.EqualTo((int)HttpStatusCode.Unauthorized));
            Assert.That(ex.DocumentationUrl, Is.Not.Null);
            Assert.That(ex.Errors?.Any(), Is.True);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RawResponse, Is.Not.Null.And.Not.Empty);
            Assert.That(ex.RequestId, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task ThrowsOnConflict()
        {
            // given
            var options = new CreatePaymentOptions
            {
                Amount = 500,
                ChargeDate = DateTime.Now.AddMonths(1),
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

            var configuration = ApiClientConfiguration.ForSandbox(_accessToken, throwOnConflict: true);
            var subject = new ApiClient(configuration);
            var paymentsClient = new PaymentsClient(subject);

            // when
            var result = await paymentsClient.CreateAsync(options);
            AsyncTestDelegate test = () => paymentsClient.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ConflictingResourceException>(test);
            var error = ex.Errors.Single(x => x.Reason == "idempotent_creation_conflict");
            Assert.That(error.Links["conflicting_resource_id"], Is.EqualTo(result.Item.Id));
        }

        [Test]
        public async Task FetchesOnConflict()
        {
            // given
            var options = new CreatePaymentOptions
            {
                Amount = 500,
                ChargeDate = DateTime.Now.AddMonths(1),
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

            var configuration = ApiClientConfiguration.ForSandbox(_accessToken, throwOnConflict: false);
            var subject = new ApiClient(configuration);
            var paymentsClient = new PaymentsClient(subject);

            // when
            var result = await paymentsClient.CreateAsync(options);
            AsyncTestDelegate test = () => paymentsClient.CreateAsync(options);

            // then
            Assert.DoesNotThrowAsync(test);

            Assert.That(result.Item.Id, Is.Not.Null);
            Assert.That(result.Item.Amount, Is.EqualTo(options.Amount));
            Assert.That(result.Item.AmountRefunded, Is.Not.Null);
            Assert.That(result.Item.ChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Currency, Is.EqualTo(options.Currency));
            Assert.That(result.Item.Description, Is.EqualTo(options.Description));
            Assert.That(result.Item.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(result.Item.Links.Mandate, Is.EqualTo(_mandate.Id));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Reference, Is.EqualTo(options.Reference));
            Assert.That(result.Item.Status, Is.Not.Null.And.Not.EqualTo(PaymentStatus.Cancelled));
        }
    }
}