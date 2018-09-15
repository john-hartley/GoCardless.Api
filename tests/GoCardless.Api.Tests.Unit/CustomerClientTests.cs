﻿using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Customers;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class CustomerClientTests
    {
        private ClientConfiguration _clientConfiguration;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            _clientConfiguration = ClientConfiguration.ForLive("accesstoken");
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public async Task CallsAllCustomersEndpoint()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            // when
            await subject.AllAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customers")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void AllCustomersRequestIsNullThrows()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            AllCustomersRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsAllCustomersEndpointUsingRequest()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            var request = new AllCustomersRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.AllAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customers?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void CreateCustomerRequestIsNullThrows()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            CreateCustomerRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsCreateCustomerEndpoint()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            var request = new CreateCustomerRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await subject.CreateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customers")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CustomerIdIsNullOrWhiteSpaceThrows(string customerId)
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(customerId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(customerId)));
        }

        [Test]
        public async Task CallsIndividualCustomersEndpoint()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);
            var customerId = "CU12345678";

            // when
            await subject.ForIdAsync(customerId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customers/CU12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateCustomerRequestIsNullThrows()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            UpdateCustomerRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void UpdateCustomerRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            var request = new UpdateCustomerRequest();

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsUpdateCustomerEndpoint()
        {
            // given
            var subject = new CustomersClient(_clientConfiguration);

            var request = new UpdateCustomerRequest
            {
                Id = "CU12345678"
            };

            // when
            await subject.UpdateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customers")
                .WithVerb(HttpMethod.Put);
        }
    }
}