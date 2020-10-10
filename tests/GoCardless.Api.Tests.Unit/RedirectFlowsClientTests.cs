using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.RedirectFlows;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class RedirectFlowsClientTests
    {
        private IRedirectFlowsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ClientConfiguration.ForLive("accesstoken"));
            _subject = new RedirectFlowsClient(apiClient);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void CompleteRedirectFlowOptionsIsNullThrows()
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
        public async Task CallsCompleteRedirectFlowEndpointUsingOptions()
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
        public void CreateRedirectFlowOptionsIsNullThrows()
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
        public async Task CallsCreateRedirectFlowEndpoint()
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
        public async Task CallsIndividualRedirectFlowsEndpoint()
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