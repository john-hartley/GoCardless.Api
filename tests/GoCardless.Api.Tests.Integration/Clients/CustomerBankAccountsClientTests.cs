using GoCardlessApi.Tests.Integration.TestHelpers;
using GoCardlessApi.CustomerBankAccounts;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class CustomerBankAccountsClientTests : IntegrationTest
    {
        private ICustomerBankAccountsClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new CustomerBankAccountsClient(_configuration);
        }

        [Test]
        public async Task creates_and_disables_conflicting_customer_bank_account_using_branch_code()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer();

            var createOptions = new CreateCustomerBankAccountOptions
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CustomerBankAccountLinks { Customer = customer.Id },
                Metadata = Metadata.Initial
            };

            // when
            await _subject.CreateAsync(createOptions);
            var createResult = await _subject.CreateAsync(createOptions);

            var disableOptions = new DisableCustomerBankAccountOptions
            {
                Id = createResult.Item.Id
            };

            var disableResult = await _subject.DisableAsync(disableOptions);

            // then
            Assert.That(createResult.Item.Id, Is.Not.Null);
            Assert.That(createResult.Item.AccountHolderName, Is.EqualTo(createOptions.AccountHolderName));
            Assert.That(createResult.Item.AccountNumberEnding, Is.Not.Null);
            Assert.That(createResult.Item.BankName, Is.Not.Null);
            Assert.That(createResult.Item.CountryCode, Is.EqualTo(createOptions.CountryCode));
            Assert.That(createResult.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(createResult.Item.Currency, Is.EqualTo(createOptions.Currency));
            Assert.That(createResult.Item.Metadata, Is.EqualTo(createOptions.Metadata));
            Assert.That(createResult.Item.Links.Customer, Is.EqualTo(createOptions.Links.Customer));
            Assert.That(createResult.Item.Enabled, Is.True);

            Assert.That(disableResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task creates_american_customer_bank_account()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer("US", "en", "WA");

            var createOptions = new CreateCustomerBankAccountOptions
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "2715500356",
                AccountType = "checking",
                BankCode = "026073150",
                CountryCode = "US",
                Currency = "USD",
                Links = new CustomerBankAccountLinks { Customer = customer.Id },
                Metadata = Metadata.Initial
            };

            // when
            var createResult = await _subject.CreateAsync(createOptions);

            var result = await _subject.ForIdAsync(createResult.Item.Id);

            var disableOptions = new DisableCustomerBankAccountOptions
            {
                Id = createResult.Item.Id
            };

            var disableResult = await _subject.DisableAsync(disableOptions);

            // then
            Assert.That(createResult.Item.Id, Is.Not.Null);
            Assert.That(createResult.Item.AccountHolderName, Is.EqualTo(createOptions.AccountHolderName));
            Assert.That(createResult.Item.AccountType, Is.EqualTo(createOptions.AccountType));

            Assert.That(result.Item.AccountType, Is.EqualTo(createOptions.AccountType));

            Assert.That(disableResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task returns_customer_bank_accounts()
        {
            // given
            // when
            var result = (await _subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].AccountHolderName, Is.Not.Null);
            Assert.That(result[0].AccountNumberEnding, Is.Not.Null);
            Assert.That(result[0].BankName, Is.Not.Null);
            Assert.That(result[0].CountryCode, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].Links.Customer, Is.Not.Null);
        }

        [Test]
        public async Task maps_paging_properties()
        {
            // given
            var firstPageOptions = new GetCustomerBankAccountsOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetCustomerBankAccountsOptions
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await _subject.GetPageAsync(secondPageOptions);

            // then
            Assert.That(firstPageResult.Items.Count(), Is.EqualTo(firstPageOptions.Limit));
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageOptions.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);

            Assert.That(secondPageResult.Items.Count(), Is.EqualTo(secondPageOptions.Limit));
            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageOptions.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
        }

        [Test]
        public async Task returns_customer_bank_account()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);

            // when
            var result = await _subject.ForIdAsync(customerBankAccount.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(customerBankAccount.Id));
            Assert.That(actual.AccountHolderName, Is.Not.Null.And.EqualTo(customerBankAccount.AccountHolderName));
            Assert.That(actual.AccountNumberEnding, Is.Not.Null.And.EqualTo(customerBankAccount.AccountNumberEnding));
            Assert.That(actual.BankName, Is.Not.Null.And.EqualTo(customerBankAccount.BankName));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(customerBankAccount.CountryCode));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(customerBankAccount.Currency));
            Assert.That(actual.Links.Customer, Is.Not.Null.And.EqualTo(customerBankAccount.Links.Customer));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(customerBankAccount.Metadata));
            Assert.That(actual.Enabled, Is.EqualTo(customerBankAccount.Enabled));
        }

        [Test]
        public async Task updates_customer_bank_account_preserving_metadata()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);

            var options = new UpdateCustomerBankAccountOptions
            {
                Id = customerBankAccount.Id
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(customerBankAccount.Metadata));
        }

        [Test]
        public async Task updates_customer_bank_account_replacing_metadata()
        {
            // given
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);

            var options = new UpdateCustomerBankAccountOptions
            {
                Id = customerBankAccount.Id,
                Metadata = Metadata.Updated
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(options.Metadata));
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task pages_through_customer_bank_accounts()
        {
            // given
            var firstId = (await _subject.GetPageAsync()).Items.First().Id;

            var options = new GetCustomerBankAccountsOptions
            {
                After = firstId,
                CreatedGreaterThan = new DateTimeOffset(DateTime.Now.AddDays(-1))
            };

            // when
            var result = await _subject
                .PageUsing(options)
                .GetItemsAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}