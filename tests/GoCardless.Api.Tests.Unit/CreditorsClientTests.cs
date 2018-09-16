using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Creditors;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class CreditorsClientTests
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

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void CreditorIdIsNullOrWhiteSpaceThrows(string creditorId)
        {
            // given
            var subject = new CreditorsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(creditorId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(creditorId)));
        }

        [Test]
        public async Task CallsIndividualCreditorsEndpoint()
        {
            // given
            var subject = new CreditorsClient(_clientConfiguration);
            var creditorId = "CR12345678";

            // when
            await subject.ForIdAsync(creditorId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditors/CR12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetCreditorsEndpoint()
        {
            // given
            var subject = new CreditorsClient(_clientConfiguration);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditors")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetCreditorsRequestIsNullThrows()
        {
            // given
            var subject = new CreditorsClient(_clientConfiguration);

            GetCreditorsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsGetCreditorsEndpointUsingRequest()
        {
            // given
            var subject = new CreditorsClient(_clientConfiguration);

            var request = new GetCreditorsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditors?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void UpdateCreditorRequestIsNullThrows()
        {
            // given
            var subject = new CreditorsClient(_clientConfiguration);

            UpdateCreditorRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public void UpdateCreditorRequestIdIsNullEmptyOrWhiteSpaceThrows()
        {
            // given
            var subject = new CreditorsClient(_clientConfiguration);

            var request = new UpdateCreditorRequest();

            // when
            AsyncTestDelegate test = () => subject.UpdateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request.Id)));
        }

        [Test]
        public async Task CallsUpdateCreditorEndpoint()
        {
            // given
            var subject = new CreditorsClient(_clientConfiguration);

            var request = new UpdateCreditorRequest
            {
                Id = "CR12345678"
            };

            // when
            await subject.UpdateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/creditors")
                .WithVerb(HttpMethod.Put);
        }
    }
}