using GoCardlessApi.Core;
using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.Customers;
using GoCardlessApi.Mandates;
using GoCardlessApi.Payments;
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

        public PaymentsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
        }

        [Test]
        public async Task CreatesAndCancelsPayment()
        {
            // given
            var creditor = await Creditor();
            var customer = await CreateCustomer();
            var customerBankAccount = await CreateCustomerBankAccountFor(customer);
            var mandate = await CreateMandate(creditor, customer, customerBankAccount);

            var createRequest = new CreatePaymentRequest
            {
                Amount = 500,
                //AppFee = 50,
                ChargeDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Description = "Sandbox Payment",
                Currency = "GBP",
                Links = new CreatePaymentLinks { Mandate = mandate.Id },
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
            Assert.That(creationResult.Payment.Links.Creditor, Is.EqualTo(creditor.Id));
            Assert.That(creationResult.Payment.Links.Mandate, Is.EqualTo(mandate.Id));
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
            var payment = (await subject.AllAsync()).Payments.First();

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
        public async Task UpdatesPayment()
        {
            // given
            var creditor = await Creditor();
            var customer = await CreateCustomer();
            var customerBankAccount = await CreateCustomerBankAccountFor(customer);
            var mandate = await CreateMandate(creditor, customer, customerBankAccount);
            var payment = await CreatePaymentFor(mandate);

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

        [Test, Explicit]
        public async Task RetriesPayment()
        {
            // given
            var creditor = await Creditor();
            var customer = await CreateCustomer();
            var customerBankAccount = await CreateCustomerBankAccountFor(customer);
            var mandate = await CreateMandate(creditor, customer, customerBankAccount);
            var payment = await CreatePaymentFor(mandate);

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

        private async Task<Payment> CreatePaymentFor(Mandate mandate)
        {
            var request = new CreatePaymentRequest
            {
                Amount = 500,
                //AppFee = 50,
                ChargeDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Description = "Sandbox Payment",
                Currency = "GBP",
                Links = new CreatePaymentLinks { Mandate = mandate.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Reference = "REF123456"
            };

            var paymentsClient = new PaymentsClient(_configuration);
            
            return (await paymentsClient.CreateAsync(request)).Payment;
        }

        private async Task<Mandate> CreateMandate(
            Creditor creditor,
            Customer customer, 
            CustomerBankAccount customerBankAccount)
        {
            var mandatesClient = new MandatesClient(_configuration);

            var request = new CreateMandateRequest
            {
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                //Reference = "REF12345678",
                Scheme = "bacs",
                Links = new CreateMandateLinks
                {
                    Creditor = creditor.Id,
                    CustomerBankAccount = customerBankAccount.Id
                }
            };

            return (await mandatesClient.CreateAsync(request)).Mandate;
        }

        private async Task<CustomerBankAccount> CreateCustomerBankAccountFor(Customer customer)
        {
            var customerBankAccountsClient = new CustomerBankAccountsClient(_configuration);

            var request = new CreateCustomerBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CustomerBankAccountLinks { Customer = customer.Id }
            };

            return (await customerBankAccountsClient.CreateAsync(request)).CustomerBankAccount;
        }

        private async Task<Customer> CreateCustomer()
        {
            var customersClient = new CustomersClient(_configuration);

            var request = new CreateCustomerRequest
            {
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name",
                CountryCode = "GB",
                Email = "email@example.com",
                FamilyName = "Family Name",
                GivenName = "Given Name",
                Language = "en",
                PostCode = "SW1A 1AA",
                Region = "Essex"
            };

            return (await customersClient.CreateAsync(request)).Customer;
        }

        private async Task<Creditor> Creditor()
        {
            var creditorsClient = new CreditorsClient(_configuration);
            return (await creditorsClient.AllAsync()).Creditors.First();
        }
    }
}