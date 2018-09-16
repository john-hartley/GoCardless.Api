using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Events;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class EventsClientTests
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
        public void EventIdIsNullOrWhiteSpaceThrows(string eventId)
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            // when
            AsyncTestDelegate test = () => subject.ForIdAsync(eventId);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.Message, Is.Not.Null);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(eventId)));
        }

        [Test]
        public async Task CallsIndividualEventsEndpoint()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);
            var eventId = "EV12345678";

            // when
            await subject.ForIdAsync(eventId);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/events/EV12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetEventsEndpoint()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            // when
            await subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/events")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetEventsRequestIsNullThrows()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            GetEventsRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.GetPageAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsGetEventsEndpointUsingRequest()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var request = new GetEventsRequest
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await subject.GetPageAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/events?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }
    }
}