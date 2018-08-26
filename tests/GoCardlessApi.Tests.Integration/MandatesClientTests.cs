﻿using GoCardlessApi.Core;
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
        [Test]
        public async Task CreatesMandate()
        {
            // given
            var creditorsClient = new CreditorsClient(ClientConfiguration.ForSandbox(_accessToken));
            var creditor = (await creditorsClient.AllAsync()).Creditors.First();

            var customer = await CreateCustomer();
            var customerBankAccount = await CreateCustomerBankAccountFor(customer);

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

            var subject = new MandatesClient(ClientConfiguration.ForSandbox(_accessToken));

            // when
            var result = await subject.CreateAsync(request);
            var actual = result.Mandate;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Links.Creditor, Is.EqualTo(creditor.Id));
            Assert.That(actual.Links.CustomerBankAccount, Is.EqualTo(customerBankAccount.Id));
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(actual.NextPossibleChargeDate, Is.Not.EqualTo(default(DateTime)));
            //Assert.That(actual.Reference, Is.EqualTo(request.Reference));
            Assert.That(actual.Scheme, Is.EqualTo(request.Scheme));
            Assert.That(actual.Status, Is.Not.Null);
        }

        private async Task<CustomerBankAccount> CreateCustomerBankAccountFor(Customer customer)
        {
            var customerBankAccountsClient = new CustomerBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));

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
            var customersClient = new CustomersClient(ClientConfiguration.ForSandbox(_accessToken));

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
    }
}