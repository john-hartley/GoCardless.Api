using GoCardless.Api.CustomerBankAccounts;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class CustomerBankAccountsTests : IntegrationTest
    {
        [Test]
        public async Task CreatesAndDisablesConflictingCustomerBankAccountUsingBranchCode()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer();

            var createRequest = new CreateCustomerBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CustomerBankAccountLinks { Customer = customer.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                }
            };

            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            // when
            await subject.CreateAsync(createRequest);
            var creationResult = await subject.CreateAsync(createRequest);

            var disableRequest = new DisableCustomerBankAccountRequest
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
            Assert.That(creationResult.Item.Links.Customer, Is.EqualTo(createRequest.Links.Customer));
            Assert.That(creationResult.Item.Enabled, Is.True);

            Assert.That(disabledResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task ReturnsCustomerBankAccounts()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            // when
            var result = (await subject.GetPageAsync()).Items.ToList();

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
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            var firstPageRequest = new GetCustomerBankAccountsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.GetPageAsync(firstPageRequest);

            var secondPageRequest = new GetCustomerBankAccountsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await subject.GetPageAsync(secondPageRequest);

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
        public async Task ReturnsIndividualCustomerBankAccount()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);

            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            // when
            var result = await subject.ForIdAsync(customerBankAccount.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(customerBankAccount.Id));
            Assert.That(actual.AccountHolderName, Is.Not.Null.And.EqualTo(customerBankAccount.AccountHolderName));
            Assert.That(actual.AccountNumberEnding, Is.Not.Null.And.EqualTo(customerBankAccount.AccountNumberEnding));
            Assert.That(actual.BankName, Is.Not.Null.And.EqualTo(customerBankAccount.BankName));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(customerBankAccount.CountryCode));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(customerBankAccount.Currency));
            Assert.That(actual.Links.Customer, Is.Not.Null.And.EqualTo(customerBankAccount.Links.Customer));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(customerBankAccount.Metadata));
            Assert.That(actual.Enabled, Is.EqualTo(customerBankAccount.Enabled));
        }

        [Test]
        public async Task UpdatesCustomerBankAccountPreservingMetadata()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);

            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            var request = new UpdateCustomerBankAccountRequest
            {
                Id = customerBankAccount.Id
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(customerBankAccount.Metadata));
        }

        [Test]
        public async Task UpdatesCustomerBankAccountReplacingMetadata()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);

            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            var request = new UpdateCustomerBankAccountRequest
            {
                Id = customerBankAccount.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }

        [Test, Explicit("Can end up performing lots of calls.")]
        public async Task PagesThroughCustomerBankAccounts()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);
            var firstId = (await subject.GetPageAsync()).Items.First().Id;

            var initialRequest = new GetCustomerBankAccountsRequest
            {
                After = firstId,
                CreatedGreaterThan = new DateTimeOffset(DateTime.Now.AddDays(-1)),
                Limit = 1,
            };

            // when
            var result = await subject
                .BuildPager()
                .StartFrom(initialRequest)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}