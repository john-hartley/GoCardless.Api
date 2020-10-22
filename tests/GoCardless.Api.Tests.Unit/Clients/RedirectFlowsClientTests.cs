using Flurl.Http.Testing;
using GoCardlessApi.RedirectFlows;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class RedirectFlowsClientTests
    {
        private IRedirectFlowsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new RedirectFlowsClient(configuration);
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
            TestDelegate test = () => new RedirectFlowsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_complete_redirect_flow_options_not_provided()
        {
            // given
            CompleteRedirectFlowOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CompleteAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_complete_redirect_flow_endpoint_using_options()
        {
            // given
            var options = new CompleteRedirectFlowOptions
            {
                Id = "RE12345678",
                SessionToken = "SE12345678"
            };

            // when
            await _subject.CompleteAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/redirect_flows/RE12345678/actions/complete")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void throws_when_create_redirect_flow_options_not_provided()
        {
            // given
            CreateRedirectFlowOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_create_redirect_flow_endpoint()
        {
            // given
            var options = new CreateRedirectFlowOptions();

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/redirect_flows")
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
        public async Task calls_get_redirect_flow_endpoint()
        {
            // given
            var id = "RE12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/redirect_flows/RE12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}