using GoCardless.Api.Core.Configuration;
using NUnit.Framework;
using System;

namespace GoCardless.Api.Tests.Unit
{
    public class ClientConfigurationTests
    {
        private readonly string _accessToken;

        public ClientConfigurationTests()
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
            TestDelegate test = () => ClientConfiguration.ForLive(accessToken);

            // then
            var ex = Assert.Throws<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(accessToken)));
        }

        [Test]
        public void BaseUriIsLiveUriWhenUsingLiveConfiguration()
        {
            // given
            var subject = ClientConfiguration.ForLive(_accessToken);

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
            TestDelegate test = () => ClientConfiguration.ForSandbox(accessToken);

            // then
            var ex = Assert.Throws<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(accessToken)));
        }

        [Test]
        public void BaseUriIsSandboxUriWhenUsingSandboxConfiguration()
        {
            // given
            var subject = ClientConfiguration.ForSandbox(_accessToken);

            // when
            var result = subject.BaseUri;

            // then
            Assert.That(result, Is.EqualTo("https://api-sandbox.gocardless.com/"));
        }
    }
}