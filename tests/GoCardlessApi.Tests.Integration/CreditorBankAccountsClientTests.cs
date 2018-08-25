using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class CreditorBankAccountsClientTests : IntegrationTest
    {
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
            Assert.That(creationResult.CreditorBankAccount.AccountNumberEnding, Is.Not.Null.And.Not.Empty);
            Assert.That(creationResult.CreditorBankAccount.BankName, Is.Not.Null.And.Not.Empty);
            Assert.That(creationResult.CreditorBankAccount.CountryCode, Is.EqualTo(createRequest.CountryCode));
            Assert.That(creationResult.CreditorBankAccount.Currency, Is.EqualTo(createRequest.Currency));
            Assert.That(creationResult.CreditorBankAccount.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.CreditorBankAccount.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(creationResult.CreditorBankAccount.Enabled, Is.True);
            Assert.That(disabledResult.CreditorBankAccount.Enabled, Is.False);
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
            Assert.That(creationResult.CreditorBankAccount.AccountNumberEnding, Is.Not.Null.And.Not.Empty);
            Assert.That(creationResult.CreditorBankAccount.BankName, Is.Not.Null.And.Not.Empty);
            Assert.That(creationResult.CreditorBankAccount.CountryCode, Is.EqualTo(createRequest.CountryCode));
            Assert.That(creationResult.CreditorBankAccount.Currency, Is.EqualTo(createRequest.Currency));
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
            Assert.That(result[0].Id, Is.Not.Null.And.Not.Empty);
            Assert.That(result[0].AccountHolderName, Is.Not.Null.And.Not.Empty);
            Assert.That(result[0].AccountNumberEnding, Is.Not.Null.And.Not.Empty);
            Assert.That(result[0].BankName, Is.Not.Null.And.Not.Empty);
            Assert.That(result[0].CountryCode, Is.Not.Null.And.Not.Empty);
            Assert.That(result[0].Currency, Is.Not.Null.And.Not.Empty);
            Assert.That(result[0].Links.Creditor, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public async Task ReturnsIndividualCreditorBankAccount()
        {
            // given
            var subject = new CreditorBankAccountsClient(ClientConfiguration.ForSandbox(_accessToken));
            var creditorBankAccount = (await subject.AllAsync()).CreditorBankAccounts.First();

            // when
            var result = await subject.ForIdAsync(creditorBankAccount.Id);
            var returnedAccount = result.CreditorBankAccount;

            // then
            Assert.That(returnedAccount, Is.Not.Null);
            Assert.That(returnedAccount.Id, Is.Not.Null.And.Not.Empty.And.EqualTo(creditorBankAccount.Id));
            Assert.That(returnedAccount.AccountHolderName, Is.Not.Null.And.Not.Empty.And.EqualTo(creditorBankAccount.AccountHolderName));
            Assert.That(returnedAccount.AccountNumberEnding, Is.Not.Null.And.Not.Empty.And.EqualTo(creditorBankAccount.AccountNumberEnding));
            Assert.That(returnedAccount.BankName, Is.Not.Null.And.Not.Empty.And.EqualTo(creditorBankAccount.BankName));
            Assert.That(returnedAccount.CountryCode, Is.Not.Null.And.Not.Empty.And.EqualTo(creditorBankAccount.CountryCode));
            Assert.That(returnedAccount.Currency, Is.Not.Null.And.Not.Empty.And.EqualTo(creditorBankAccount.Currency));
            Assert.That(returnedAccount.Links.Creditor, Is.Not.Null.And.Not.Empty.And.EqualTo(creditorBankAccount.Links.Creditor));
            Assert.That(returnedAccount.Enabled, Is.EqualTo(creditorBankAccount.Enabled));
        }
    }
}