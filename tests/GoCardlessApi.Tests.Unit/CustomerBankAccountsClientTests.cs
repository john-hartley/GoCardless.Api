using Flurl.Http.Testing;
using GoCardlessApi.Core;
using GoCardlessApi.CustomerBankAccounts;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit
{
    public class CustomerBankAccountsClientTests
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
        public async Task CallsAllCustomerBankAccountsEndpoint()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            // when
            await subject.AllAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void AllCustomerBankAccountsRequestIsNullThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            AllCustomerBankAccountsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsAllCustomerBankAccountsEndpointUsingRequest()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            var request = new AllCustomerBankAccountsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.AllAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void CreateCustomerBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

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
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            var request = new CreateCustomerBankAccountRequest();

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
        public void CustomerBankAccountIdIsNullOrWhiteSpaceThrows(string customerBankAccountId)
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(customerBankAccountId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(customerBankAccountId)));
        }

        [Test]
        public async Task CallsIndividualCustomerBankAccountsEndpoint()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);
            var customerBankAccountId = "BA12345678";

            // when
            await subject.ForIdAsync(customerBankAccountId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts/BA12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void DisableCustomerBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            DisableCustomerBankAccountRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.DisableAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void DisableCustomerBankAccountRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            var request = new DisableCustomerBankAccountRequest();

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
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

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
        public void UpdateCustomerBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            UpdateCustomerBankAccountRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void UpdateCustomerBankAccountRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

            var request = new UpdateCustomerBankAccountRequest();

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
            var subject = new CustomerBankAccountsClient(_clientConfiguration);

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