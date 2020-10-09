using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.CustomerBankAccounts;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class CustomerBankAccountsClientTests
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
        public void CreateCustomerBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            CreateCustomerBankAccountRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsCreateCustomerBankAccountEndpoint()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            var request = new CreateCustomerBankAccountRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await subject.CreateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualCustomerBankAccountsEndpoint()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);
            var id = "BA12345678";

            // when
            await subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts/BA12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void DisableCustomerBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            DisableCustomerBankAccountRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.DisableAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void DisableCustomerBankAccountRequestIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            var request = new DisableCustomerBankAccountRequest
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => subject.DisableAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsDisableCustomerBankAccountEndpoint()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            var request = new DisableCustomerBankAccountRequest
            {
                Id = "BA12345678"
            };

            // when
            await subject.DisableAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts/BA12345678/actions/disable")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public async Task CallsGetCustomerBankAccountsEndpoint()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetCustomerBankAccountsRequestIsNullThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            GetCustomerBankAccountsRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetCustomerBankAccountsEndpointUsingRequest()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            var request = new GetCustomerBankAccountsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateCustomerBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            UpdateCustomerBankAccountRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateCustomerBankAccountRequestIdIsNullEmptyOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            var request = new UpdateCustomerBankAccountRequest
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsUpdateCustomerBankAccountEndpoint()
        {
            // given
            var subject = new CustomerBankAccountsClient(_apiClient, _apiClient.Configuration);

            var request = new UpdateCustomerBankAccountRequest
            {
                Id = "BA12345678"
            };

            // when
            await subject.UpdateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts")
                .WithVerb(HttpMethod.Put);
        }
    }
}