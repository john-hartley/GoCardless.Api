using GoCardless.Api.MandateImportEntries;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class MandateImportEntriesClientTests : IntegrationTest
    {
        [Test]
        public async Task CreatesMandateImportEntries()
        {
            // given
            var subject = new MandateImportEntriesClient(_clientConfiguration);
            var mandateImport = await _resourceFactory.CreateMandateImport();

            var request = new AddMandateImportEntriesRequest
            {
                BankAccount = new BankAccount
                {
                    AccountHolderName = "Joe Bloggs",
                    AccountNumber = "55666666",
                    BranchCode = "200000",
                    CountryCode = "GB"
                },
                Customer = new Customer
                {
                    AddressLine1 = "Address Line 1",
                    AddressLine2 = "Address Line 2",
                    AddressLine3 = "Address Line 3",
                    City = "London",
                    CompanyName = "Company Name",
                    CountryCode = "DK",
                    Email = "email@example.com",
                    FamilyName = "Family Name",
                    GivenName = "Given Name",
                    Language = "da",
                    DanishIdentityNumber = "2205506218",
                    SwedishIdentityNumber = "5302256218",
                    PostalCode = "SW1A 1AA",
                    Region = "Essex",
                },
                RecordIdentifier = $"import-{DateTime.Now:yyyyMMddhhssmmfff}",
                Links = new MandateImportEntriesLinks
                {
                    MandateImport = mandateImport.Id
                }
            };

            // when
            var result = await subject.AddAsync(request);
            var actual = result.MandateImportEntry;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.MandateImport, Is.Not.Null);
            Assert.That(actual.RecordIdentifier, Is.Not.Null);
        }
    }
}
