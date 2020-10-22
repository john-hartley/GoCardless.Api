using NUnit.Framework;
using System;

namespace GoCardlessApi.Tests.Unit
{
    public class GoCardlessConfigurationTests
    {
        private readonly string _accessToken = "accesstoken";

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_access_token_is_not_provided_for_live_configuration(string accessToken)
        {
            // given
            // when
            TestDelegate test = () => GoCardlessConfiguration.ForLive(accessToken, false);

            // then
            var ex = Assert.Throws<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(accessToken)));
        }

        [Test]
        public void uses_live_uri_for_live_configuration()
        {
            // given
            var subject = GoCardlessConfiguration.ForLive(_accessToken, false);

            // when
            var result = subject.BaseUri;

            // then
            Assert.That(result, Is.EqualTo("https://api.gocardless.com/"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_access_token_is_not_provided_for_sandbox_configuration(string accessToken)
        {
            // given
            // when
            TestDelegate test = () => GoCardlessConfiguration.ForSandbox(accessToken, false);

            // then
            var ex = Assert.Throws<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(accessToken)));
        }

        [Test]
        public void uses_sandbox_uri_for_sandbox_configuration()
        {
            // given
            var subject = GoCardlessConfiguration.ForSandbox(_accessToken, false);

            // when
            var result = subject.BaseUri;

            // then
            Assert.That(result, Is.EqualTo("https://api-sandbox.gocardless.com/"));
        }
    }
}