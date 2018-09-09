using Flurl.Http.Testing;
using GoCardless.Api.Core;
using GoCardless.Api.CreditorBankAccounts;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class CreditorBankAccountsClientTests
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
        public async Task CallsAllCreditorBankAccountsEndpoint()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            // when
            await subject.AllAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void AllCreditorBankAccountsRequestIsNullThrows()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            AllCreditorBankAccountsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.AllAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsAllCreditorBankAccountsEndpointUsingRequest()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            var request = new AllCreditorBankAccountsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.AllAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void CreateCreditorBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            CreateCreditorBankAccountRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsCreateCreditorBankAccountEndpoint()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            var request = new CreateCreditorBankAccountRequest
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await subject.CreateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void DisableCreditorBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            DisableCreditorBankAccountRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.DisableAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void DisableCreditorBankAccountRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            var request = new DisableCreditorBankAccountRequest();

            // when
            AsyncTestDelegate test = () => subject.DisableAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsDisableCreditorBankAccountEndpoint()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            var request = new DisableCreditorBankAccountRequest
            {
                Id = "BA12345678"
            };

            // when
            await subject.DisableAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts/BA12345678/actions/disable")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CreditorBankAccountIdIsNullOrWhiteSpaceThrows(string creditorBankAccountId)
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(creditorBankAccountId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(creditorBankAccountId)));
        }

        [Test]
        public async Task CallsIndividualCreditorBankAccountsEndpoint()
        {
            // given
            var subject = new CreditorBankAccountsClient(_clientConfiguration);
            var creditorBankAccountId = "BA12345678";

            // when
            await subject.ForIdAsync(creditorBankAccountId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts/BA12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}