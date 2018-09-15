﻿using GoCardless.Api.CreditorBankAccounts;
using GoCardless.Api.Creditors;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class CreditorBankAccountsClientTests : IntegrationTest
    {
        private Creditor _creditor;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _creditor = await _resourceFactory.Creditor();
        }

        [Test]
        public async Task CreatesAndDisablesCreditorBankAccountUsingBankCode()
        {
            // given
            var createRequest = new CreateCreditorBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "532013001",
                BankCode = "37040044",
                CountryCode = "DE",
                Currency = "EUR",
                Links = new CreditorBankAccountLinks { Creditor = _creditor.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                SetAsDefaultPayoutAccount = true
            };

            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var disableRequest = new DisableCreditorBankAccountRequest
            {
                Id = creationResult.Item.Id
            };

            var disabledResult = await subject.DisableAsync(disableRequest);

            // then
            Assert.That(creationResult.Item.Id, Is.Not.Null);
            Assert.That(creationResult.Item.AccountHolderName, Is.EqualTo(createRequest.AccountHolderName));
            Assert.That(creationResult.Item.AccountNumberEnding, Is.Not.Null);
            Assert.That(creationResult.Item.BankName, Is.Not.Null);
            Assert.That(creationResult.Item.CountryCode, Is.EqualTo(createRequest.CountryCode));
            Assert.That(creationResult.Item.Currency, Is.EqualTo(createRequest.Currency));
            Assert.That(creationResult.Item.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.Item.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(creationResult.Item.Enabled, Is.True);

            Assert.That(disabledResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task CreatesAndDisablesCreditorBankAccountUsingBranchCode()
        {
            // given
            var createRequest = new CreateCreditorBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CreditorBankAccountLinks { Creditor = _creditor.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                }
            };

            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var disableRequest = new DisableCreditorBankAccountRequest
            {
                Id = creationResult.Item.Id
            };

            var disabledResult = await subject.DisableAsync(disableRequest);

            // then
            Assert.That(creationResult.Item.Id, Is.Not.Null.And.Not.Empty);
            Assert.That(creationResult.Item.AccountHolderName, Is.EqualTo(createRequest.AccountHolderName));
            Assert.That(creationResult.Item.AccountNumberEnding, Is.Not.Null);
            Assert.That(creationResult.Item.BankName, Is.Not.Null.And.Not.Empty);
            Assert.That(creationResult.Item.CountryCode, Is.EqualTo(createRequest.CountryCode));
            Assert.That(creationResult.Item.Currency, Is.EqualTo(createRequest.Currency));
            Assert.That(creationResult.Item.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.Item.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(creationResult.Item.Enabled, Is.True);

            Assert.That(disabledResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task CreatesAndDisablesCreditorBankAccountUsingIban()
        {
            // given
            var createRequest = new CreateCreditorBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                Iban = "GB60 BARC 2000 0055 7799 11",
                Links = new CreditorBankAccountLinks { Creditor = _creditor.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                }
            };

            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var disableRequest = new DisableCreditorBankAccountRequest
            {
                Id = creationResult.Item.Id
            };

            var disabledResult = await subject.DisableAsync(disableRequest);

            // then
            Assert.That(creationResult.Item.Id, Is.Not.Null);
            Assert.That(creationResult.Item.AccountHolderName, Is.EqualTo(createRequest.AccountHolderName));
            Assert.That(creationResult.Item.AccountNumberEnding, Is.Not.Null);
            Assert.That(creationResult.Item.BankName, Is.Not.Null);
            Assert.That(creationResult.Item.CountryCode, Is.Not.Null);
            Assert.That(creationResult.Item.Currency, Is.Not.Null);
            Assert.That(creationResult.Item.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.Item.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(creationResult.Item.Enabled, Is.True);

            Assert.That(disabledResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task ReturnsCreditorBankAccounts()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            // when
            var result = (await subject.AllAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].AccountHolderName, Is.Not.Null);
            Assert.That(result[0].AccountNumberEnding, Is.Not.Null);
            Assert.That(result[0].BankName, Is.Not.Null);
            Assert.That(result[0].CountryCode, Is.Not.Null);
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].Links.Creditor, Is.Not.Null);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            var firstPageRequest = new AllCreditorBankAccountsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.AllAsync(firstPageRequest);

            var secondPageRequest = new AllCreditorBankAccountsRequest
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
        public async Task ReturnsIndividualCreditorBankAccount()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);
            var creditorBankAccount = (await subject.AllAsync()).Items.First();

            // when
            var result = await subject.ForIdAsync(creditorBankAccount.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(creditorBankAccount.Id));
            Assert.That(actual.AccountHolderName, Is.Not.Null.And.EqualTo(creditorBankAccount.AccountHolderName));
            Assert.That(actual.AccountNumberEnding, Is.Not.Null.And.EqualTo(creditorBankAccount.AccountNumberEnding));
            Assert.That(actual.BankName, Is.Not.Null.And.EqualTo(creditorBankAccount.BankName));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(creditorBankAccount.CountryCode));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(creditorBankAccount.Currency));
            Assert.That(actual.Links.Creditor, Is.Not.Null.And.EqualTo(creditorBankAccount.Links.Creditor));
            Assert.That(actual.Enabled, Is.EqualTo(creditorBankAccount.Enabled));
        }
    }
}