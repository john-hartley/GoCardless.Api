using GoCardless.Api.Core.Http;
using NUnit.Framework;
using System;

namespace GoCardless.Api.Tests.Unit
{
    public class GoCardlessClientTests
    {
        [Test]
        public void ApiClientIsNullThrows()
        {
            // given
            IApiClient apiClient = null;

            // when
            TestDelegate test = () => new GoCardlessClient(apiClient);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClient)));
        }

        [Test]
        public void ApiClientConfigurationIsNullThrows()
        {
            // given
            ApiClientConfiguration apiClientConfiguration = null;

            // when
            TestDelegate test = () => new GoCardlessClient(apiClientConfiguration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(apiClientConfiguration)));
        }
    }
}