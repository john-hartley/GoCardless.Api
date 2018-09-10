﻿using Flurl.Http.Testing;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.MandatePdfs;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit
{
    public class MandatePdfsClientTests
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

        [Test]
        public void CreateMandatePdfRequestIsNullThrows()
        {
            // given
            var subject = new MandatePdfsClient(_clientConfiguration);

            CreateMandatePdfRequest request = null;

            // when
            AsyncTestDelegate test = () => subject.CreateAsync(request);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(request)));
        }

        [Test]
        public async Task CallsCreateMandatePdfEndpointWithoutAcceptsLanguageHeader()
        {
            // given
            var subject = new MandatePdfsClient(_clientConfiguration);

            var request = new CreateMandatePdfRequest
            {
                Links = new MandatePdfLinks
                {
                    Mandate = "MD12345678"
                }
            };

            // when
            await subject.CreateAsync(request);

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
            var subject = new MandatePdfsClient(_clientConfiguration);

            var request = new CreateMandatePdfRequest
            {
                Language = "en",
                Links = new MandatePdfLinks
                {
                    Mandate = "MD12345678"
                }
            };

            // when
            await subject.CreateAsync(request);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_pdfs")
                .WithHeader("Accept-Language", request.Language)
                .WithVerb(HttpMethod.Post);
        }
    }
}