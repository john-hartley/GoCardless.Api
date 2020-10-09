using GoCardless.Api.MandateImportEntries;
using GoCardless.Api.MandateImports;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class MandateImportEntriesClientTests : IntegrationTest
    {
        [Test]
        public async Task CreatesMandateImportEntries()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient, _apiClient.Configuration);
            var mandateImport = await _resourceFactory.CreateMandateImport();

            var request = new AddMandateImportEntryRequest
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
                    DanishIdentityNumber = "2205506218",
                    Email = "email@example.com",
                    FamilyName = "Family Name",
                    GivenName = "Given Name",
                    Language = "da",
                    PhoneNumber = "+44 1234 567890",
                    PostalCode = "SW1A 1AA",
                    Region = "Essex",
                    SwedishIdentityNumber = "5302256218",
                },
                Links = new AddMandateImportEntryLinks
                {
                    MandateImport = mandateImport.Id
                },
                RecordIdentifier = $"import-{DateTime.Now:yyyyMMddhhssmmfff}"
            };

            // when
            var result = await subject.AddAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.MandateImport, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(actual.RecordIdentifier, Is.Not.Null);
        }

        [Test, Explicit("There is a short delay between the submission of a mandate import and it being processed. Once processed, the properties should be mapped.")]
        public async Task MapsAllLinksAfterMandateImportHasBeenSubmitted()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient, _apiClient.Configuration);
            var mandateImport = await _resourceFactory.CreateMandateImport();
            var mandateImportEntry = await _resourceFactory.CreateMandateImportEntryFor(mandateImport, "first-record");

            var mandateImportsClient = new MandateImportsClient(_apiClient);
            await mandateImportsClient.SubmitAsync(mandateImport.Id);

            var request = new GetMandateImportEntriesRequest
            {
                MandateImport = mandateImport.Id
            };

            // when
            var result = (await subject.GetPageAsync(request)).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Customer, Is.Not.Null);
            Assert.That(result[0].Links.CustomerBankAccount, Is.Not.Null);
            Assert.That(result[0].Links.Mandate, Is.Not.Null);
            Assert.That(result[0].Links.MandateImport, Is.Not.Null.And.EqualTo(mandateImport.Id));
        }

        [Test]
        public async Task ReturnsAllMandateImportEntries()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient, _apiClient.Configuration);
            var mandateImport = await _resourceFactory.CreateMandateImport();

            var firstRecordId = "first-record";
            var firstEntry = await _resourceFactory.CreateMandateImportEntryFor(mandateImport, firstRecordId);

            var secondRecordId = "second-record";
            var secondEntry = await _resourceFactory.CreateMandateImportEntryFor(mandateImport, secondRecordId);

            var request = new GetMandateImportEntriesRequest
            {
                MandateImport = mandateImport.Id
            };

            // when
            var result = (await subject.GetPageAsync(request)).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);

            var firstReturnedEntry = result.SingleOrDefault(x => x.RecordIdentifier == firstRecordId);
            var secondReturnedEntry = result.SingleOrDefault(x => x.RecordIdentifier == secondRecordId);

            Assert.That(firstReturnedEntry, Is.Not.Null);
            Assert.That(firstReturnedEntry.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(firstReturnedEntry.Links, Is.Not.Null);
            Assert.That(firstReturnedEntry.Links.MandateImport, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(firstReturnedEntry.RecordIdentifier, Is.EqualTo(firstRecordId));

            Assert.That(secondReturnedEntry, Is.Not.Null);
            Assert.That(secondReturnedEntry.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(secondReturnedEntry.Links, Is.Not.Null);
            Assert.That(secondReturnedEntry.Links.MandateImport, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(secondReturnedEntry.RecordIdentifier, Is.EqualTo(secondRecordId));
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient, _apiClient.Configuration);
            var mandateImport = await _resourceFactory.CreateMandateImport();

            var firstRecordId = "first-record";
            await _resourceFactory.CreateMandateImportEntryFor(mandateImport, firstRecordId);

            var secondRecordId = "second-record";
            await _resourceFactory.CreateMandateImportEntryFor(mandateImport, secondRecordId);

            var firstPageRequest = new GetMandateImportEntriesRequest
            {
                Limit = 1,
                MandateImport = mandateImport.Id
            };

            // when
            var firstPageResult = await subject.GetPageAsync(firstPageRequest);

            var secondPageRequest = new GetMandateImportEntriesRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1,
                MandateImport = mandateImport.Id
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
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Null);
        }

        [Test]
        public async Task PagesThroughMandateImportEntries()
        {
            // given
            var subject = new MandateImportEntriesClient(_apiClient, _apiClient.Configuration);
            var mandateImport = await _resourceFactory.CreateMandateImport();

            var firstRecordId = "first-record";
            await _resourceFactory.CreateMandateImportEntryFor(mandateImport, firstRecordId);

            var secondRecordId = "second-record";
            await _resourceFactory.CreateMandateImportEntryFor(mandateImport, secondRecordId);

            var initialRequest = new GetMandateImportEntriesRequest
            {
                Limit = 1,
                MandateImport = mandateImport.Id
            };

            // when
            var result = await subject
                .BuildPager()
                .StartFrom(initialRequest)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].RecordIdentifier, Is.Not.Null.And.Not.EqualTo(result[1].RecordIdentifier));
            Assert.That(result[1].RecordIdentifier, Is.Not.Null.And.Not.EqualTo(result[0].RecordIdentifier));
        }
    }
}