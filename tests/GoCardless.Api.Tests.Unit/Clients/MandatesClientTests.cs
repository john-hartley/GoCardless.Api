using Flurl.Http.Testing;
using GoCardlessApi.Mandates;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class MandatesClientTests
    {
        private IMandatesClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new MandatesClient(configuration);
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
            TestDelegate test = () => new MandatesClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_cancel_mandate_options_not_provided()
        {
            // given
            CancelMandateOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_cancel_mandate_id_not_provided(string id)
        {
            // given
            var options = new CancelMandateOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task calls_cancel_mandate_endpoint()
        {
            // given
            var options = new CancelMandateOptions
            {
                Id = "MD12345678"
            };

            // when
            await _subject.CancelAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void throws_when_create_mandate_options_not_provided()
        {
            // given
            CreateMandateOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_create_mandate_endpoint()
        {
            // given
            var options = new CreateMandateOptions
            {
                IdempotencyKey = Guid.NewGuid().ToString()
            };

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates")
                .WithHeader("Idempotency-Key")
                .WithVerb(HttpMethod.Post);
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
        public async Task calls_get_mandate_endpoint()
        {
            // given
            var id = "MD12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task calls_get_mandates_endpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_get_mandates_options_not_provided()
        {
            // given
            GetMandatesOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_get_mandates_endpoint_using_options()
        {
            // given
            var options = new GetMandatesOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_reinstate_mandates_options_not_provided()
        {
            // given
            ReinstateMandateOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.ReinstateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_reinstate_mandates_id_not_provided(string id)
        {
            // given
            var options = new ReinstateMandateOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.ReinstateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task calls_reinstate_mandate_endpoint()
        {
            // given
            var options = new ReinstateMandateOptions
            {
                Id = "MD12345678"
            };

            // when
            await _subject.ReinstateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates/MD12345678/actions/reinstate")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void throws_when_update_mandates_options_not_provided()
        {
            // given
            UpdateMandateOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.UpdateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_update_mandates_id_not_provided(string id)
        {
            // given
            var options = new UpdateMandateOptions
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
        public async Task calls_update_mandate_endpoint()
        {
            // given
            var options = new UpdateMandateOptions
            {
                Id = "MD12345678"
            };

            // when
            await _subject.UpdateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandates")
                .WithVerb(HttpMethod.Put);
        }
    }
}