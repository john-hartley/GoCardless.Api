using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class CreditorBankAccountsClientTests : IntegrationTest
    {
        [Test]
        public async Task CreatesCreditorBankAccount()
        {
            // given
            var request = new CreateCreditorBankAccountRequest
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
            var result = await subject.CreateAsync(request);

            // then
            Assert.That(result.CreditorBankAccount.Id, Is.Not.Null);
            Assert.That(result.CreditorBankAccount.AccountHolderName, Is.EqualTo(request.AccountHolderName));
            Assert.That(result.CreditorBankAccount.AccountNumber, Is.EqualTo(request.AccountNumber));
            Assert.That(result.CreditorBankAccount.BranchCode, Is.EqualTo(request.BranchCode));
            Assert.That(result.CreditorBankAccount.CountryCode, Is.EqualTo(request.CountryCode));
            Assert.That(result.CreditorBankAccount.Currency, Is.EqualTo(request.Currency));
            Assert.That(result.CreditorBankAccount.Links.Creditor, Is.EqualTo(request.Links.Creditor));
        }
    }
}