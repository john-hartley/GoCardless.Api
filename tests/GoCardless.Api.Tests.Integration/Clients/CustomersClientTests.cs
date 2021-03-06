﻿using GoCardlessApi.Customers;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class CustomersClientTests : IntegrationTest
    {
        private ICustomersClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new CustomersClient(_configuration);
        }

        [Test]
        public async Task creates_conflicting_customer()
        {
            // given
            var options = new CreateCustomerOptions
            {
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name",
                CountryCode = "NZ",
                DanishIdentityNumber = "2205506218",
                Email = "email@example.com",
                FamilyName = "Family Name",
                GivenName = "Given Name",
                Language = "en",
                Metadata = Metadata.Initial,
                PhoneNumber = "+44 20 7183 8674",
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                SwedishIdentityNumber = "5302256218",
            };

            // when
            await _subject.CreateAsync(options);
            var result = await _subject.CreateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.AddressLine1, Is.EqualTo(options.AddressLine1));
            Assert.That(actual.AddressLine2, Is.EqualTo(options.AddressLine2));
            Assert.That(actual.AddressLine3, Is.EqualTo(options.AddressLine3));
            Assert.That(actual.City, Is.EqualTo(options.City));
            Assert.That(actual.CompanyName, Is.EqualTo(options.CompanyName));
            Assert.That(actual.CountryCode, Is.EqualTo(options.CountryCode));
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.DanishIdentityNumber, Is.EqualTo(options.DanishIdentityNumber));
            Assert.That(actual.Email, Is.EqualTo(options.Email));
            Assert.That(actual.FamilyName, Is.EqualTo(options.FamilyName));
            Assert.That(actual.GivenName, Is.EqualTo(options.GivenName));
            Assert.That(actual.Language, Is.EqualTo(options.Language));
            Assert.That(actual.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(actual.PhoneNumber, Is.EqualTo(options.PhoneNumber));
            Assert.That(actual.PostalCode, Is.EqualTo(options.PostalCode));
            Assert.That(actual.Region, Is.EqualTo(options.Region));
            Assert.That(actual.SwedishIdentityNumber, Does.Contain(options.SwedishIdentityNumber));
        }

        [Test]
        public async Task returns_customers()
        {
            // given
            // when
            var result = (await _subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].AddressLine1, Is.Not.Null);
            Assert.That(result[0].AddressLine2, Is.Not.Null);
            Assert.That(result[0].AddressLine3, Is.Not.Null);
            Assert.That(result[0].City, Is.Not.Null);
            Assert.That(result[0].CompanyName, Is.Not.Null);
            Assert.That(result[0].CountryCode, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].DanishIdentityNumber, Is.Not.Null);
            Assert.That(result[0].Email, Is.Not.Null);
            Assert.That(result[0].FamilyName, Is.Not.Null);
            Assert.That(result[0].GivenName, Is.Not.Null);
            Assert.That(result[0].Language, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].PhoneNumber, Is.Not.Null);
            Assert.That(result[0].PostalCode, Is.Not.Null);
            Assert.That(result[0].Region, Is.Not.Null);
            Assert.That(result[0].SwedishIdentityNumber, Is.Not.Null);
        }

        [Test]
        public async Task maps_paging_properties()
        {
            // given
            var firstPageOptions = new GetCustomersOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetCustomersOptions
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
        public async Task returns_customer()
        {
            // given
            var customer = await _resourceFactory.CreateNzCustomer();

            // when
            var result = await _subject.ForIdAsync(customer.Id);
            var actual = result.Item;

            // then
            Validate(actual, customer);
            Assert.That(actual.DanishIdentityNumber, Is.Not.Null.And.EqualTo(customer.DanishIdentityNumber));
            Assert.That(actual.SwedishIdentityNumber, Is.Not.Null.And.EqualTo(customer.SwedishIdentityNumber));
        }

        [Test]
        public async Task returns_customer_for_new_zealand()
        {
            // given
            var customer = await _resourceFactory.CreateNzCustomer();

            // when
            var result = await _subject.ForIdAsync(customer.Id);
            var actual = result.Item;

            // then
            Validate(actual, customer);
        }

        [Test]
        public async Task updates_customer_preserving_metadata()
        {
            // given
            var customer = await _resourceFactory.CreateNzCustomer();

            var options = new UpdateCustomerOptions
            {
                Id = customer.Id,
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name 2",
                CountryCode = "NZ",
                DanishIdentityNumber = "2205506218",
                Email = "email@example.com",
                FamilyName = "Family Name 2",
                GivenName = "Given Name 2",
                Language = "en",
                PhoneNumber = "+44 1235 567890",
                PostalCode = "SW1A 1AA",
                Region = "Region",
                SwedishIdentityNumber = "5302256218",
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Validate(actual, options);
            Assert.That(actual.Metadata, Is.EqualTo(customer.Metadata));
        }

        [Test]
        public async Task updates_customer_replacing_metadata()
        {
            // given
            var customer = await _resourceFactory.CreateNzCustomer();

            var options = new UpdateCustomerOptions
            {
                Id = customer.Id,
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name 2",
                CountryCode = "NZ",
                DanishIdentityNumber = "2205506218",
                Email = "email@example.com",
                FamilyName = "Family Name 2",
                GivenName = "Given Name 22",
                Language = "en",
                Metadata = Metadata.Updated,
                PhoneNumber = "+44 1235 567890",
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                SwedishIdentityNumber = "5302256218",
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Validate(actual, options);
            Assert.That(actual.Metadata, Is.EqualTo(options.Metadata));
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task pages_through_customers()
        {
            // given
            var items = (await _subject.GetPageAsync()).Items;
            var first = items.First();
            var last = items.Last();

            var options = new GetCustomersOptions
            {
                After = first.Id,
                CreatedGreaterThan = last.CreatedAt.AddDays(-1)
            };

            // when
            var result = await _subject
                .PageUsing(options)
                .GetItemsAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result.Any(x => x.Id == first.Id), Is.False);
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }

        private static void Validate(Customer actual, Customer expected)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.AddressLine1, Is.Not.Null.And.EqualTo(expected.AddressLine1));
            Assert.That(actual.AddressLine2, Is.Not.Null.And.EqualTo(expected.AddressLine2));
            Assert.That(actual.AddressLine3, Is.Not.Null.And.EqualTo(expected.AddressLine3));
            Assert.That(actual.City, Is.Not.Null.And.EqualTo(expected.City));
            Assert.That(actual.CompanyName, Is.Not.Null.And.EqualTo(expected.CompanyName));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(expected.CountryCode));
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Email, Is.Not.Null.And.EqualTo(expected.Email));
            Assert.That(actual.FamilyName, Is.Not.Null.And.EqualTo(expected.FamilyName));
            Assert.That(actual.GivenName, Is.Not.Null.And.EqualTo(expected.GivenName));
            Assert.That(actual.Language, Is.Not.Null.And.EqualTo(expected.Language));
            Assert.That(actual.PhoneNumber, Is.Not.Null.And.EqualTo(expected.PhoneNumber));
            Assert.That(actual.PostalCode, Is.Not.Null.And.EqualTo(expected.PostalCode));
            Assert.That(actual.Region, Is.Not.Null.And.EqualTo(expected.Region));
        }

        private static void Validate(Customer actual, UpdateCustomerOptions expected)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.AddressLine1, Is.EqualTo(expected.AddressLine1));
            Assert.That(actual.AddressLine2, Is.EqualTo(expected.AddressLine2));
            Assert.That(actual.AddressLine3, Is.EqualTo(expected.AddressLine3));
            Assert.That(actual.City, Is.EqualTo(expected.City));
            Assert.That(actual.CompanyName, Is.EqualTo(expected.CompanyName));
            Assert.That(actual.CountryCode, Is.EqualTo(expected.CountryCode));
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.DanishIdentityNumber, Is.EqualTo(expected.DanishIdentityNumber));
            Assert.That(actual.Email, Is.EqualTo(expected.Email));
            Assert.That(actual.FamilyName, Is.EqualTo(expected.FamilyName));
            Assert.That(actual.GivenName, Is.EqualTo(expected.GivenName));
            Assert.That(actual.Language, Is.EqualTo(expected.Language));
            Assert.That(actual.PhoneNumber, Is.EqualTo(expected.PhoneNumber));
            Assert.That(actual.PostalCode, Is.EqualTo(expected.PostalCode));
            Assert.That(actual.Region, Is.EqualTo(expected.Region));
            Assert.That(actual.SwedishIdentityNumber, Does.Contain(expected.SwedishIdentityNumber));
        }
    }
}