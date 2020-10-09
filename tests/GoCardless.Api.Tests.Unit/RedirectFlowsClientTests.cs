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
        public void CompleteRedirectFlowRequestIsNullThrows()
        {
            // given
            var subject = new RedirectFlowsClient(_apiClient);

            CompleteRedirectFlowRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CompleteAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCompleteRedirectFlowEndpointUsingRequest()
        {
            // given
            var subject = new RedirectFlowsClient(_apiClient);

            var request = new CompleteRedirectFlowRequest
            {
                Id = "RE12345678",
                SessionToken = "SE12345678"
            };

            // when
            await subject.CompleteAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/redirect_flows/RE12345678/actions/complete")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void CreateRedirectFlowRequestIsNullThrows()
        {
            // given
            var subject = new RedirectFlowsClient(_apiClient);

            CreateRedirectFlowRequest options = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateRedirectFlowEndpoint()
        {
            // given
            var subject = new RedirectFlowsClient(_apiClient);

            var request = new CreateRedirectFlowRequest();

            // when
            await subject.CreateAsync(request);

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
            var subject = new RedirectFlowsClient(_apiClient);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task CallsIndividualRedirectFlowsEndpoint()
        {
            // given
            var subject = new RedirectFlowsClient(_apiClient);
            var id = "RE12345678";

            // when
            await subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/redirect_flows/RE12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}