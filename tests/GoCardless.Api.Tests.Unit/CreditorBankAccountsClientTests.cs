using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.CreditorBankAccounts;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class CreditorBankAccountsClientTests
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
        public void CreateCreditorBankAccountRequestIsNullThrows()
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);

            CreateCreditorBankAccountRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateCreditorBankAccountEndpoint()
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);

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
            var subject = new CreditorBankAccountsClient(_apiClient);

            DisableCreditorBankAccountRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.DisableAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void DisableCreditorBankAccountRequestIdIsNullEmptyOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);

            var request = new DisableCreditorBankAccountRequest
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
        public async Task CallsDisableCreditorBankAccountEndpoint()
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);

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
        public void IdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualCreditorBankAccountsEndpoint()
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);
            var id = "BA12345678";

            // when
            await subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts/BA12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetCreditorBankAccountsEndpoint()
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetCreditorBankAccountsRequestIsNullThrows()
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);

            GetCreditorBankAccountsRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetCreditorBankAccountsEndpointUsingRequest()
        {
            // given
            var subject = new CreditorBankAccountsClient(_apiClient);

            var request = new GetCreditorBankAccountsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }
    }
}