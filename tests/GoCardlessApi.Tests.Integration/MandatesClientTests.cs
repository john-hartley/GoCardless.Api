using GoCardlessApi.Core;
using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.Customers;
using GoCardlessApi.Mandates;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class MandatesClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;

        public MandatesClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
        }

        [Test]
        public async Task CreatesCancelsAndReinstatesMandate()
        {
            // given
            var creditor = await Creditor();
            var customer = await CreateCustomer();
            var customerBankAccount = await CreateCustomerBankAccountFor(customer);

            var createRequest = new CreateMandateRequest
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

            var subject = new MandatesClient(_configuration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var cancelRequest = new CancelMandateRequest
            {
                Id = creationResult.Mandate.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            var cancellationResult = await subject.CancelAsync(cancelRequest);

            var reinstateRequest = new ReinstateMandateRequest
            {
                Id = creationResult.Mandate.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key7"] = "Value7",
                    ["Key8"] = "Value8",
                    ["Key9"] = "Value9",
                },
            };

            var reinstateResult = (await subject.ReinstateAsync(reinstateRequest));

            // then
            Assert.That(creationResult.Mandate, Is.Not.Null);
            Assert.That(creationResult.Mandate.Id, Is.Not.Null);
            Assert.That(creationResult.Mandate.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(creationResult.Mandate.Links.Creditor, Is.EqualTo(creditor.Id));
            Assert.That(creationResult.Mandate.Links.CustomerBankAccount, Is.EqualTo(customerBankAccount.Id));
            Assert.That(creationResult.Mandate.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.Mandate.NextPossibleChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            //Assert.That(actual.Reference, Is.EqualTo(request.Reference));
            Assert.That(creationResult.Mandate.Scheme, Is.EqualTo(createRequest.Scheme));
            Assert.That(creationResult.Mandate.Status, Is.Not.Null.And.Not.EqualTo("cancelled"));
            //Assert.That(cancellationResult.Mandate.Metadata, Is.EqualTo(cancelRequest.Metadata));
            Assert.That(cancellationResult.Mandate.Status, Is.EqualTo("cancelled"));
            Assert.That(reinstateResult.Mandate.Status, Is.Not.Null.And.Not.EqualTo("cancelled"));
        }

        [Test]
        public async Task ReturnsMandates()
        {
            // given
            var subject = new MandatesClient(_configuration);

            // when
            var result = (await subject.AllAsync()).Mandates.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Links.Creditor, Is.Not.Null);
            Assert.That(result[0].Links.CustomerBankAccount, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].NextPossibleChargeDate, Is.Not.EqualTo(default(DateTime)));
            //Assert.That(result[0].Reference, Is.EqualTo(request.Reference));
            Assert.That(result[0].Scheme, Is.Not.Null);
            Assert.That(result[0].Status, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualMandate()
        {
            // given
            var subject = new MandatesClient(_configuration);
            var mandate = (await subject.AllAsync()).Mandates.First();

            // when
            var result = await subject.ForIdAsync(mandate.Id);
            var actual = result.Mandate;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(mandate.Id));
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Links.Creditor, Is.Not.Null.And.EqualTo(mandate.Links.Creditor));
            Assert.That(actual.Links.Customer, Is.Not.Null.And.EqualTo(mandate.Links.Customer));
            Assert.That(actual.Links.CustomerBankAccount, Is.Not.Null.And.EqualTo(mandate.Links.CustomerBankAccount));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(mandate.Metadata));
            Assert.That(actual.NextPossibleChargeDate, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Reference, Is.Not.Null);
            Assert.That(actual.Scheme, Is.Not.Null.And.EqualTo(mandate.Scheme));
            Assert.That(actual.Status, Is.Not.Null.And.EqualTo(mandate.Status));
        }

        [Test]
        public async Task UpdatesMandate()
        {
            // given
            var subject = new MandatesClient(_configuration);
            var mandate = (await subject.AllAsync()).Mandates.First();

            var request = new UpdateMandateRequest
            {
                Id = mandate.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Mandate;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
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