using Flurl.Http.Testing;
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
        private ICreditorBankAccountsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken"));
            _subject = new CreditorBankAccountsClient(apiClient);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void CreateCreditorBankAccountOptionsIsNullThrows()
        {
            // given
            CreateCreditorBankAccountOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateCreditorBankAccountEndpoint()
        {
            // given
            var options = new CreateCreditorBankAccountOptions
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void DisableCreditorBankAccountOptionsIsNullThrows()
        {
            // given
            DisableCreditorBankAccountOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.DisableAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void DisableCreditorBankAccountOptionsIdIsNullEmptyOrWhiteSpaceThrows(string id)
        {
            // given
            var options = new DisableCreditorBankAccountOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.DisableAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task CallsDisableCreditorBankAccountEndpoint()
        {
            // given
            var options = new DisableCreditorBankAccountOptions
            {
                Id = "BA12345678"
            };

            // when
            await _subject.DisableAsync(options);

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
            // when
            AsyncTestDelegate test = () => _subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualCreditorBankAccountsEndpoint()
        {
            // given
            var id = "BA12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts/BA12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetCreditorBankAccountsEndpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetCreditorBankAccountsOptionsIsNullThrows()
        {
            // given
            GetCreditorBankAccountsOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetCreditorBankAccountsEndpointUsingOptions()
        {
            // given
            var options = new GetCreditorBankAccountsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditor_bank_accounts?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }
    }
}