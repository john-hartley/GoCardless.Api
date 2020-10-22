using Flurl.Http.Testing;
using GoCardlessApi.Creditors;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class CreditorsClientTests
    {
        private ICreditorsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new CreditorsClient(configuration);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void throws_when_configuration_not_provided()
        {
            // given
            GoCardlessConfiguration configuration = null;

            // when
            TestDelegate test = () => new CreditorsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_id_not_provided(string id)
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
        public async Task calls_get_creditor_endpoint()
        {
            // given
            var id = "CR12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditors/CR12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task calls_get_creditors_endpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditors")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_get_creditors_options_not_provided()
        {
            // given
            GetCreditorsOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_get_creditors_endpoint_using_options()
        {
            // given
            var options = new GetCreditorsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditors?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_update_creditor_options_not_provided()
        {
            // given
            UpdateCreditorOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_update_creditor_id_not_provided(string id)
        {
            // given
            var options = new UpdateCreditorOptions
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
        public async Task calls_update_creditor_endpoint()
        {
            // given
            var options = new UpdateCreditorOptions
            {
                Id = "CR12345678"
            };

            // when
            await _subject.UpdateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditors")
                .WithVerb(HttpMethod.Put);
        }
    }
}