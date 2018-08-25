using GoCardlessApi.Core;
using GoCardlessApi.Customers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class CustomersClientTests : IntegrationTest
    {
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
                PostCode = "SW1A 1AA",
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

            var subject = new CustomersClient(ClientConfiguration.ForSandbox(_accessToken));

            // when
            var result = await subject.CreateAsync(request);
            var customer = result.Customer;

            // then
            Assert.That(customer, Is.Not.Null);
            Assert.That(customer.Id, Is.Not.Null.And.Not.Empty);
            Assert.That(customer.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(customer.Email, Is.EqualTo(request.Email));
            Assert.That(customer.GivenName, Is.EqualTo(request.GivenName));
            Assert.That(customer.FamilyName, Is.EqualTo(request.FamilyName));
            Assert.That(customer.AddressLine1, Is.EqualTo(request.AddressLine1));
            Assert.That(customer.AddressLine2, Is.EqualTo(request.AddressLine2));
            Assert.That(customer.AddressLine3, Is.EqualTo(request.AddressLine3));
            Assert.That(customer.City, Is.EqualTo(request.City));
            Assert.That(customer.Region, Is.EqualTo(request.Region));
            Assert.That(customer.PostCode, Is.EqualTo(request.PostCode));
            Assert.That(customer.CountryCode, Is.EqualTo(request.CountryCode));
            Assert.That(customer.Language, Is.EqualTo(request.Language));
            Assert.That(customer.DanishIdentityNumber, Is.EqualTo(request.DanishIdentityNumber));
            Assert.That(customer.SwedishIdentityNumber, Does.Contain(request.SwedishIdentityNumber));
            Assert.That(customer.Metadata, Is.EqualTo(request.Metadata));
        }
    }
}