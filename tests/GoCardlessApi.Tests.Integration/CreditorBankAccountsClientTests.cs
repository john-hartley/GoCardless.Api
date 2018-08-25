using NUnit.Framework;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class CreditorBankAccountsClientTests : IntegrationTest
    {
        [Test]
        public async Task CreatesAndDisablesCreditorBankAccount()
        {
            // given
            var createRequest = new CreateCreditorBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CreditorBankAccountLinks { Creditor = "CR00005N9ZWBFK" }
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
            Assert.That(creationResult.CreditorBankAccount.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(creationResult.CreditorBankAccount.Enabled, Is.True);
            Assert.That(disabledResult.CreditorBankAccount.Enabled, Is.False);
        }
    }
}