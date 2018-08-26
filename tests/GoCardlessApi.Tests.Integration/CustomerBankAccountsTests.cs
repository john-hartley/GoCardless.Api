﻿using GoCardlessApi.Core;
using GoCardlessApi.CustomerBankAccounts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class CustomerBankAccountsTests : IntegrationTest
    {
        [Test]
        public async Task CreatesAndDisablesCustomerBankAccountUsingBranchCode()
        {
            // given
            var createRequest = new CreateCustomerBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CustomerBankAccountLinks { Customer = "CU000439RS7SA2" },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                }
            };

            var subject = new CustomerBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var disableRequest = new DisableCustomerBankAccountRequest
            {
                Id = creationResult.CustomerBankAccount.Id
            };

            var disabledResult = await subject.DisableAsync(disableRequest);

            // then
            Assert.That(creationResult.CustomerBankAccount.Id, Is.Not.Null);
            Assert.That(creationResult.CustomerBankAccount.AccountHolderName, Is.EqualTo(createRequest.AccountHolderName));
            Assert.That(creationResult.CustomerBankAccount.AccountNumberEnding, Is.Not.Null);
            Assert.That(creationResult.CustomerBankAccount.BankName, Is.Not.Null);
            Assert.That(creationResult.CustomerBankAccount.CountryCode, Is.EqualTo(createRequest.CountryCode));
            Assert.That(creationResult.CustomerBankAccount.Currency, Is.EqualTo(createRequest.Currency));
            Assert.That(creationResult.CustomerBankAccount.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.CustomerBankAccount.Links.Customer, Is.EqualTo(createRequest.Links.Customer));
            Assert.That(creationResult.CustomerBankAccount.Enabled, Is.True);
            Assert.That(disabledResult.CustomerBankAccount.Enabled, Is.False);
        }

        [Test]
        public async Task ReturnsCustomerBankAccounts()
        {
            // given
            var subject = new CustomerBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));

            // when
            var result = (await subject.AllAsync()).CustomerBankAccounts.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].AccountHolderName, Is.Not.Null);
            Assert.That(result[0].AccountNumberEnding, Is.Not.Null);
            Assert.That(result[0].BankName, Is.Not.Null);
            Assert.That(result[0].CountryCode, Is.Not.Null);
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].Links.Customer, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualCustomerBankAccount()
        {
            // given
            var subject = new CustomerBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));
            var customerBankAccount = (await subject.AllAsync()).CustomerBankAccounts.First();

            // when
            var result = await subject.ForIdAsync(customerBankAccount.Id);
            var actual = result.CustomerBankAccount;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(customerBankAccount.Id));
            Assert.That(actual.AccountHolderName, Is.Not.Null.And.EqualTo(customerBankAccount.AccountHolderName));
            Assert.That(actual.AccountNumberEnding, Is.Not.Null.And.EqualTo(customerBankAccount.AccountNumberEnding));
            Assert.That(actual.BankName, Is.Not.Null.And.EqualTo(customerBankAccount.BankName));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(customerBankAccount.CountryCode));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(customerBankAccount.Currency));
            Assert.That(actual.Links.Customer, Is.Not.Null.And.EqualTo(customerBankAccount.Links.Customer));
            Assert.That(actual.Enabled, Is.EqualTo(customerBankAccount.Enabled));
        }

        [Test]
        public async Task UpdatesCustomer()
        {
            // given
            var subject = new CustomerBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));
            var customerBankAccount = (await subject.AllAsync()).CustomerBankAccounts.First();

            var request = new UpdateCustomerBankAccountRequest
            {
                Id = customerBankAccount.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.CustomerBankAccount;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }
    }
}