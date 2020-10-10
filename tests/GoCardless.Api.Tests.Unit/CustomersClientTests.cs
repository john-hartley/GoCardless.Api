﻿using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.Customers;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class CustomersClientTests
    {
        private IApiClient _apiClient;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            _apiClient = new ApiClient(ClientConfiguration.ForLive("accesstoken"));
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void CreateCustomerRequestIsNullThrows()
        {
            // given
            var subject = new CustomersClient(_apiClient);

            CreateCustomerRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateCustomerEndpoint()
        {
            // given
            var subject = new CustomersClient(_apiClient);

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
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new CustomersClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualCustomersEndpoint()
        {
            // given
            var subject = new CustomersClient(_apiClient);
            var id = "CU12345678";

            // when
            await subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customers/CU12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetCustomersEndpoint()
        {
            // given
            var subject = new CustomersClient(_apiClient);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customers")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetCustomersRequestIsNullThrows()
        {
            // given
            var subject = new CustomersClient(_apiClient);

            GetCustomersRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetCustomersEndpointUsingRequest()
        {
            // given
            var subject = new CustomersClient(_apiClient);

            var request = new GetCustomersRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customers?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateCustomerRequestIsNullThrows()
        {
            // given
            var subject = new CustomersClient(_apiClient);

            UpdateCustomerRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateCustomerRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new CustomersClient(_apiClient);

            var options = new UpdateCustomerRequest
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task CallsUpdateCustomerEndpoint()
        {
            // given
            var subject = new CustomersClient(_apiClient);

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