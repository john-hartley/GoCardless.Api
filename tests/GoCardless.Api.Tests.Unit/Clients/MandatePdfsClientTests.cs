using Flurl.Http.Testing;
using GoCardlessApi.MandatePdfs;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class MandatePdfsClientTests
    {
        private IMandatePdfsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new MandatePdfsClient(configuration);
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
            TestDelegate test = () => new MandatePdfsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void CreateMandatePdfOptionsIsNullThrows()
        {
            // given
            CreateMandatePdfOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsCreateMandatePdfEndpointWithoutAcceptsLanguageHeader()
        {
            // given
            var options = new CreateMandatePdfOptions
            {
                Links = new MandatePdfLinks
                {
                    Mandate = "MD12345678"
                }
            };

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_pdfs")
                .WithoutHeader("Accept-Language")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public async Task CallsCreateMandatePdfEndpointWithAcceptsLanguageHeader()
        {
            // given
            var options = new CreateMandatePdfOptions
            {
                Language = "en",
                Links = new MandatePdfLinks
                {
                    Mandate = "MD12345678"
                }
            };

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_pdfs")
                .WithHeader("Accept-Language", options.Language)
                .WithVerb(HttpMethod.Post);
        }
    }
}