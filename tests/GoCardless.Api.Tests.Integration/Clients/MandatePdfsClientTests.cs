using GoCardlessApi.MandatePdfs;
using GoCardlessApi.Common;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class MandatePdfsClientTests : IntegrationTest
    {
        private IMandatePdfsClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new MandatePdfsClient(_configuration);
        }

        [Test]
        public async Task creates_mandate_pdf_using_linked_mandate()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            var mandate = await _resourceFactory.CreateMandateFor(creditor, customerBankAccount);

            var options = new CreateMandatePdfOptions
            {
                Language = "en",
                Links = new MandatePdfLinks
                {
                    Mandate = mandate.Id
                }
            };

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.ExpiresAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Url, Is.Not.Null);
        }

        [Test]
        public async Task creates_mandate_pdf_using_customer_details()
        {
            // given
            var options = new CreateMandatePdfOptions
            {
                AccountHolderName = "Account holder",
                AccountNumber = "44779911",
                AddressLine1 = "Address line 1",
                AddressLine2 = "Address line 2",
                AddressLine3 = "Address line 3",
                BankCode = "Bank code",
                Bic = "Bic",
                City = "City",
                CountryCode = "Country code",
                DanishIdentityNumber = "2205506218",
                Iban = "GB18 BARC 1234 5678",
                Language = "en",
                MandateReference = "MR12345678",
                PhoneNumber = "+44 20 7183 8674",
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                Scheme = Scheme.BecsNz,
                SignatureDate = DateTime.Now,
                SwedishIdentityNumber = "5302256218",
            };

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.ExpiresAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Url, Is.Not.Null);
        }
    }
}