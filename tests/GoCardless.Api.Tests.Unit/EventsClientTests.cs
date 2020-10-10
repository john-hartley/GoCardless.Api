using Flurl.Http.Testing;
using GoCardless.Api.Core.Http;
using GoCardless.Api.Events;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class EventsClientTests
    {
        private IEventsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ApiClientConfiguration.ForLive("accesstoken"));
            _subject = new EventsClient(apiClient);
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
        public void EventIdIsNullOrWhiteSpaceThrows(string id)
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
        public async Task CallsIndividualEventsEndpoint()
        {
            // given
            var id = "EV12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/events/EV12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public async Task CallsGetEventsEndpoint()
        {
            // given
            // when
            await _subject.GetPageAsync();

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/events")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void GetEventsOptionsIsNullThrows()
        {
            // given
            GetEventsOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsGetEventsEndpointUsingOptions()
        {
            // given
            var options = new GetEventsOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/events?before=before%20test&after=after%20test&limit=5")
                .WithVerb(HttpMethod.Get);
        }
    }
}