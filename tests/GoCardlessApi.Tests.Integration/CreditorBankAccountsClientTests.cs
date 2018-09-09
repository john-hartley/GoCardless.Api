using GoCardless.Api.Core;
using GoCardless.Api.CreditorBankAccounts;
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
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        private Creditor _creditor;

        public CreditorBankAccountsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

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

            var subject = new CreditorBankAccountsClient(_configuration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var disableRequest = new DisableCreditorBankAccountRequest
            {
                Id = creationResult.CreditorBankAccount.Id
            };

            var disabledResult = await subject.DisableAsync(disableRequest);

            // then
            Assert.That(creationResult.CreditorBankAccount.Id, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.AccountHolderName, Is.EqualTo(createRequest.AccountHolderName));
            Assert.That(creationResult.CreditorBankAccount.AccountNumberEnding, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.BankName, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.CountryCode, Is.EqualTo(createRequest.CountryCode));
            Assert.That(creationResult.CreditorBankAccount.Currency, Is.EqualTo(createRequest.Currency));
            Assert.That(creationResult.CreditorBankAccount.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.CreditorBankAccount.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(creationResult.CreditorBankAccount.Enabled, Is.True);

            Assert.That(disabledResult.CreditorBankAccount.Enabled, Is.False);
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

            var subject = new CreditorBankAccountsClient(_configuration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var disableRequest = new DisableCreditorBankAccountRequest
            {
                Id = creationResult.CreditorBankAccount.Id
            };

            var disabledResult = await subject.DisableAsync(disableRequest);

            // then
            Assert.That(creationResult.CreditorBankAccount.Id, Is.Not.Null.And.Not.Empty);
            Assert.That(creationResult.CreditorBankAccount.AccountHolderName, Is.EqualTo(createRequest.AccountHolderName));
            Assert.That(creationResult.CreditorBankAccount.AccountNumberEnding, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.BankName, Is.Not.Null.And.Not.Empty);
            Assert.That(creationResult.CreditorBankAccount.CountryCode, Is.EqualTo(createRequest.CountryCode));
            Assert.That(creationResult.CreditorBankAccount.Currency, Is.EqualTo(createRequest.Currency));
            Assert.That(creationResult.CreditorBankAccount.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.CreditorBankAccount.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(creationResult.CreditorBankAccount.Enabled, Is.True);

            Assert.That(disabledResult.CreditorBankAccount.Enabled, Is.False);
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

            var subject = new CreditorBankAccountsClient(_configuration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var disableRequest = new DisableCreditorBankAccountRequest
            {
                Id = creationResult.CreditorBankAccount.Id
            };

            var disabledResult = await subject.DisableAsync(disableRequest);

            // then
            Assert.That(creationResult.CreditorBankAccount.Id, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.AccountHolderName, Is.EqualTo(createRequest.AccountHolderName));
            Assert.That(creationResult.CreditorBankAccount.AccountNumberEnding, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.BankName, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.CountryCode, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.Currency, Is.Not.Null);
            Assert.That(creationResult.CreditorBankAccount.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.CreditorBankAccount.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(creationResult.CreditorBankAccount.Enabled, Is.True);

            Assert.That(disabledResult.CreditorBankAccount.Enabled, Is.False);
        }

        [Test]
        public async Task ReturnsCreditorBankAccounts()
        {
            // given
            var subject = new CreditorBankAccountsClient(_configuration);

            // when
            var result = (await subject.AllAsync()).CreditorBankAccounts.ToList();

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
            var subject = new CreditorBankAccountsClient(_configuration);

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
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(firstPageResult.CreditorBankAccounts.Count(), Is.EqualTo(firstPageRequest.Limit));

            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(secondPageResult.CreditorBankAccounts.Count(), Is.EqualTo(secondPageRequest.Limit));
        }

        [Test]
        public async Task ReturnsIndividualCreditorBankAccount()
        {
            // given
            var subject = new CreditorBankAccountsClient(_configuration);
            var creditorBankAccount = (await subject.AllAsync()).CreditorBankAccounts.First();

            // when
            var result = await subject.ForIdAsync(creditorBankAccount.Id);
            var actual = result.CreditorBankAccount;

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