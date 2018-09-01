﻿using GoCardlessApi.Core;
using GoCardlessApi.Customers;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class CustomersClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        public CustomersClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

        [Test]
        public async Task CreatesCustomer()
        {
            // given
            var request = new CreateCustomerRequest
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
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                DanishIdentityNumber = "2205506218",
                SwedishIdentityNumber = "5302256218",
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
            };

            var subject = new CustomersClient(_configuration);

            // when
            var result = await subject.CreateAsync(request);
            var actual = result.Customer;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Email, Is.EqualTo(request.Email));
            Assert.That(actual.GivenName, Is.EqualTo(request.GivenName));
            Assert.That(actual.FamilyName, Is.EqualTo(request.FamilyName));
            Assert.That(actual.AddressLine1, Is.EqualTo(request.AddressLine1));
            Assert.That(actual.AddressLine2, Is.EqualTo(request.AddressLine2));
            Assert.That(actual.AddressLine3, Is.EqualTo(request.AddressLine3));
            Assert.That(actual.City, Is.EqualTo(request.City));
            Assert.That(actual.Region, Is.EqualTo(request.Region));
            Assert.That(actual.PostalCode, Is.EqualTo(request.PostalCode));
            Assert.That(actual.CountryCode, Is.EqualTo(request.CountryCode));
            Assert.That(actual.Language, Is.EqualTo(request.Language));
            Assert.That(actual.DanishIdentityNumber, Is.EqualTo(request.DanishIdentityNumber));
            Assert.That(actual.SwedishIdentityNumber, Does.Contain(request.SwedishIdentityNumber));
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }

        [Test]
        public async Task ReturnsCustomers()
        {
            // given
            var subject = new CustomersClient(_configuration);

            // when
            var result = (await subject.AllAsync()).Customers.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Email, Is.Not.Null);
            Assert.That(result[0].GivenName, Is.Not.Null);
            Assert.That(result[0].FamilyName, Is.Not.Null);
            Assert.That(result[0].AddressLine1, Is.Not.Null);
            Assert.That(result[0].AddressLine2, Is.Not.Null);
            Assert.That(result[0].AddressLine3, Is.Not.Null);
            Assert.That(result[0].City, Is.Not.Null);
            Assert.That(result[0].Region, Is.Not.Null);
            Assert.That(result[0].PostalCode, Is.Not.Null);
            Assert.That(result[0].CountryCode, Is.Not.Null);
            Assert.That(result[0].Language, Is.Not.Null);
            Assert.That(result[0].DanishIdentityNumber, Is.Not.Null);
            Assert.That(result[0].SwedishIdentityNumber, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualCustomer()
        {
            // given
            var customer = await _resourceFactory.CreateForeignCustomer();
            var subject = new CustomersClient(_configuration);

            // when
            var result = await subject.ForIdAsync(customer.Id);
            var actual = result.Customer;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Email, Is.Not.Null.And.EqualTo(customer.Email));
            Assert.That(actual.GivenName, Is.Not.Null.And.EqualTo(customer.GivenName));
            Assert.That(actual.FamilyName, Is.Not.Null.And.EqualTo(customer.FamilyName));
            Assert.That(actual.AddressLine1, Is.Not.Null.And.EqualTo(customer.AddressLine1));
            Assert.That(actual.AddressLine2, Is.Not.Null.And.EqualTo(customer.AddressLine2));
            Assert.That(actual.AddressLine3, Is.Not.Null.And.EqualTo(customer.AddressLine3));
            Assert.That(actual.City, Is.Not.Null.And.EqualTo(customer.City));
            Assert.That(actual.Region, Is.Not.Null.And.EqualTo(customer.Region));
            Assert.That(actual.PostalCode, Is.Not.Null.And.EqualTo(customer.PostalCode));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(customer.CountryCode));
            Assert.That(actual.Language, Is.Not.Null.And.EqualTo(customer.Language));
            Assert.That(actual.DanishIdentityNumber, Is.Not.Null.And.EqualTo(customer.DanishIdentityNumber));
            Assert.That(actual.SwedishIdentityNumber, Is.Not.Null.And.EqualTo(customer.SwedishIdentityNumber));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(customer.Metadata));
        }

        [Test]
        public async Task UpdatesCustomer()
        {
            // given
            var customer = await _resourceFactory.CreateForeignCustomer();
            var subject = new CustomersClient(_configuration);

            var request = new UpdateCustomerRequest
            {
                Id = customer.Id,
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
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                DanishIdentityNumber = "2205506218",
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value6",
                    ["Key5"] = "Value7",
                    ["Key6"] = "Value8",
                },
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Customer;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Email, Is.EqualTo(request.Email));
            Assert.That(actual.GivenName, Is.EqualTo(request.GivenName));
            Assert.That(actual.FamilyName, Is.EqualTo(request.FamilyName));
            Assert.That(actual.AddressLine1, Is.EqualTo(request.AddressLine1));
            Assert.That(actual.AddressLine2, Is.EqualTo(request.AddressLine2));
            Assert.That(actual.AddressLine3, Is.EqualTo(request.AddressLine3));
            Assert.That(actual.City, Is.EqualTo(request.City));
            Assert.That(actual.Region, Is.EqualTo(request.Region));
            Assert.That(actual.PostalCode, Is.EqualTo(request.PostalCode));
            Assert.That(actual.CountryCode, Is.EqualTo(request.CountryCode));
            Assert.That(actual.Language, Is.EqualTo(request.Language));
            Assert.That(actual.DanishIdentityNumber, Is.EqualTo(request.DanishIdentityNumber));
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }
    }
}