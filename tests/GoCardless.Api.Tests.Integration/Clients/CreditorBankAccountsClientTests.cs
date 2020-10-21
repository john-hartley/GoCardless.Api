using GoCardlessApi.Tests.Integration.TestHelpers;
using GoCardlessApi.CreditorBankAccounts;
using GoCardlessApi.Creditors;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class CreditorBankAccountsClientTests : IntegrationTest
    {
        private ICreditorBankAccountsClient _subject;
        private Creditor _creditor;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _creditor = await _resourceFactory.Creditor();
        }

        [SetUp]
        public void Setup()
        {
            _subject = new CreditorBankAccountsClient(_configuration);
        }

        [Test, Explicit("Checking the disabled state in the final assertion sometimes takes a little time.")]
        [Category(TestCategory.TimeDependent)]
        public async Task CreatesAndDisablesConflictingCreditorBankAccountUsingBankCode()
        {
            // given
            var createOptions = new CreateCreditorBankAccountOptions
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "532013001",
                BankCode = "37040044",
                CountryCode = "DE",
                Currency = "EUR",
                Links = new CreditorBankAccountLinks { Creditor = _creditor.Id },
                Metadata = Metadata.Initial,
                SetAsDefaultPayoutAccount = true
            };

            // when
            await _subject.CreateAsync(createOptions);
            var createResult = await _subject.CreateAsync(createOptions);

            var disableOptions = new DisableCreditorBankAccountOptions
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
            Assert.That(createResult.Item.Currency, Is.EqualTo(createOptions.Currency));
            Assert.That(createResult.Item.Metadata, Is.EqualTo(createOptions.Metadata));
            Assert.That(createResult.Item.Links.Creditor, Is.EqualTo(createOptions.Links.Creditor));
            Assert.That(createResult.Item.Enabled, Is.True);

            Assert.That(disableResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task CreatesAndDisablesCreditorBankAccountUsingBranchCode()
        {
            // given
            var createOptions = new CreateCreditorBankAccountOptions
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CreditorBankAccountLinks { Creditor = _creditor.Id },
                Metadata = Metadata.Initial
            };

            // when
            var createResult = await _subject.CreateAsync(createOptions);

            var disableOptions = new DisableCreditorBankAccountOptions
            {
                Id = createResult.Item.Id
            };

            var disableResult = await _subject.DisableAsync(disableOptions);

            // then
            Assert.That(createResult.Item.Id, Is.Not.Null.And.Not.Empty);
            Assert.That(createResult.Item.AccountHolderName, Is.EqualTo(createOptions.AccountHolderName));
            Assert.That(createResult.Item.AccountNumberEnding, Is.Not.Null);
            Assert.That(createResult.Item.BankName, Is.Not.Null.And.Not.Empty);
            Assert.That(createResult.Item.CountryCode, Is.EqualTo(createOptions.CountryCode));
            Assert.That(createResult.Item.Currency, Is.EqualTo(createOptions.Currency));
            Assert.That(createResult.Item.Metadata, Is.EqualTo(createOptions.Metadata));
            Assert.That(createResult.Item.Links.Creditor, Is.EqualTo(createOptions.Links.Creditor));
            Assert.That(createResult.Item.Enabled, Is.True);

            Assert.That(disableResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task CreatesAndDisablesCreditorBankAccountUsingIban()
        {
            // given
            var createOptions = new CreateCreditorBankAccountOptions
            {
                AccountHolderName = "API BANK ACCOUNT",
                Iban = "GB60 BARC 2000 0055 7799 11",
                Links = new CreditorBankAccountLinks { Creditor = _creditor.Id },
                Metadata = Metadata.Initial
            };

            // when
            var createResult = await _subject.CreateAsync(createOptions);

            var disableOptions = new DisableCreditorBankAccountOptions
            {
                Id = createResult.Item.Id
            };

            var disableResult = await _subject.DisableAsync(disableOptions);

            // then
            Assert.That(createResult.Item.Id, Is.Not.Null);
            Assert.That(createResult.Item.AccountHolderName, Is.EqualTo(createOptions.AccountHolderName));
            Assert.That(createResult.Item.AccountNumberEnding, Is.Not.Null);
            Assert.That(createResult.Item.BankName, Is.Not.Null);
            Assert.That(createResult.Item.CountryCode, Is.Not.Null);
            Assert.That(createResult.Item.Currency, Is.Not.Null);
            Assert.That(createResult.Item.Metadata, Is.EqualTo(createOptions.Metadata));
            Assert.That(createResult.Item.Links.Creditor, Is.EqualTo(createOptions.Links.Creditor));
            Assert.That(createResult.Item.Enabled, Is.True);

            Assert.That(disableResult.Item.Enabled, Is.False);
        }

        [Test]
        public async Task CreatesAmericanBankAccount()
        {
            // given
            var createOptions = new CreateCreditorBankAccountOptions
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "2715500356",
                AccountType = "checking",
                BankCode = "026073150",
                CountryCode = "US",
                Currency = "USD",
                Links = new CreditorBankAccountLinks { Creditor = _creditor.Id },
                Metadata = Metadata.Initial
            };

            // when
            var createResult = await _subject.CreateAsync(createOptions);

            var result = await _subject.ForIdAsync(createResult.Item.Id);

            var disableOptions = new DisableCreditorBankAccountOptions
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
        public async Task ReturnsCreditorBankAccounts()
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
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].Links.Creditor, Is.Not.Null);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var firstPageOptions = new GetCreditorBankAccountsOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetCreditorBankAccountsOptions
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
        public async Task ReturnsIndividualCreditorBankAccount()
        {
            // given
            var creditorBankAccount = (await _subject.GetPageAsync()).Items.First();

            // when
            var result = await _subject.ForIdAsync(creditorBankAccount.Id);
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

        [Test]
        [Category(TestCategory.Paging)]
        public async Task PagesThroughCreditorBankAccounts()
        {
            // given
            var firstId = (await _subject.GetPageAsync()).Items.First().Id;

            var options = new GetCreditorBankAccountsOptions
            {
                After = firstId
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