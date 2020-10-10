using GoCardless.Api.Core.Http;
using NUnit.Framework;
using System;

namespace GoCardless.Api.Tests.Unit.Core.Http
{
    public class ApiClientConfigurationTests
    {
        private readonly string _accessToken;

        public ApiClientConfigurationTests()
        {
            _accessToken = "accesstoken";
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void AccessTokenIsNotSuppliedForLiveConfigurationThrows(string accessToken)
        {
            // given
            // when
            TestDelegate test = () => ApiClientConfiguration.ForLive(accessToken);

            // then
            var ex = Assert.Throws<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(accessToken)));
        }

        [Test]
        public void BaseUriIsLiveUriWhenUsingLiveConfiguration()
        {
            // given
            var subject = ApiClientConfiguration.ForLive(_accessToken);

            // when
            var result = subject.BaseUri;

            // then
            Assert.That(result, Is.EqualTo("https://api.gocardless.com/"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void AccessTokenIsNotSuppliedForSandboxConfigurationThrows(string accessToken)
        {
            // given
            // when
            TestDelegate test = () => ApiClientConfiguration.ForSandbox(accessToken);

            // then
            var ex = Assert.Throws<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(accessToken)));
        }

        [Test]
        public void BaseUriIsSandboxUriWhenUsingSandboxConfiguration()
        {
            // given
            var subject = ApiClientConfiguration.ForSandbox(_accessToken);

            // when
            var result = subject.BaseUri;

            // then
            Assert.That(result, Is.EqualTo("https://api-sandbox.gocardless.com/"));
        }
    }
}