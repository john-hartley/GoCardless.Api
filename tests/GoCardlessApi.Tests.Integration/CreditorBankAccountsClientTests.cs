using GoCardlessApi.Core;
using GoCardlessApi.CreditorBankAccounts;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class CreditorBankAccountsClientTests : IntegrationTest
    {
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
                Links = new CreditorBankAccountLinks { Creditor = "CR00005N9ZWBFK" },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                SetAsDefaultPayoutAccount = true
            };

            var subject = new CreditorBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));

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
                Links = new CreditorBankAccountLinks { Creditor = "CR00005N9ZWBFK" },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                }
            };

            var subject = new CreditorBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));

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
                Links = new CreditorBankAccountLinks { Creditor = "CR00005N9ZWBFK" },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                }
            };

            var subject = new CreditorBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));

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
            var subject = new CreditorBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));

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
        public async Task ReturnsIndividualCreditorBankAccount()
        {
            // given
            var subject = new CreditorBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));
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