using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.MandatePdfs;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class MandatePdfsClientTests
    {
        private IMandatePdfsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var apiClient = new ApiClient(ClientConfiguration.ForLive("accesstoken"));
            _subject = new MandatePdfsClient(apiClient);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
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