using Flurl.Http.Testing;
using GoCardlessApi.CustomerBankAccounts;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class CustomerBankAccountsClientTests
    {
        private ICustomerBankAccountsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new CustomerBankAccountsClient(configuration);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void ConfigurationIsNullThrows()
        {
            // given
            GoCardlessConfiguration configuration = null;

            // when
            TestDelegate test = () => new CustomerBankAccountsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void CreateCustomerBankAccountOptionsIsNullThrows()
        {
            // given
            CreateCustomerBankAccountOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateCustomerBankAccountEndpoint()
        {
            // given
            var options = new CreateCustomerBankAccountOptions
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await _subject.CreateAsync(options);

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
            // when
            AsyncTestDelegate test = () => _subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualCustomerBankAccountsEndpoint()
        {
            // given
            var id = "BA12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts/BA12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void DisableCustomerBankAccountOptionsIsNullThrows()
        {
            // given
            DisableCustomerBankAccountOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.DisableAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void DisableCustomerBankAccountOptionsIdIsNullOrWhiteSpaceThrows(string id)
        {
            // given
            var options = new DisableCustomerBankAccountOptions
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
        public async Task CallsDisableCustomerBankAccountEndpoint()
        {
            // given
            var options = new DisableCustomerBankAccountOptions
            {
                Id = "BA12345678"
            };

            // when
            await _subject.DisableAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts/BA12345678/actions/disable")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public async Task CallsGetCustomerBankAccountsEndpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetCustomerBankAccountsOptionsIsNullThrows()
        {
            // given
            GetCustomerBankAccountsOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetCustomerBankAccountsEndpointUsingOptions()
        {
            // given
            var options = new GetCustomerBankAccountsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateCustomerBankAccountOptionsIsNullThrows()
        {
            // given
            UpdateCustomerBankAccountOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void UpdateCustomerBankAccountOptionsIdIsNullEmptyOrWhiteSpaceThrows(string id)
        {
            // given
            var options = new UpdateCustomerBankAccountOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task CallsUpdateCustomerBankAccountEndpoint()
        {
            // given
            var options = new UpdateCustomerBankAccountOptions
            {
                Id = "BA12345678"
            };

            // when
            await _subject.UpdateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/customer_bank_accounts")
                .WithVerb(HttpMethod.Put);
        }
    }
}