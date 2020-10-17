using NUnit.Framework;
using System;

namespace GoCardless.Api.Tests.Unit
{
    public class GoCardlessClientTests
    {
        [Test]
        public void ConfigurationIsNullThrows()
        {
            // given
            GoCardlessConfiguration configuration = null;

            // when
            TestDelegate test = () => new GoCardlessClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }
    }
}