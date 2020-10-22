using NUnit.Framework;
using System;

namespace GoCardlessApi.Tests.Unit
{
    public class GoCardlessClientTests
    {
        [Test]
        public void throws_when_configuration_not_provided()
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