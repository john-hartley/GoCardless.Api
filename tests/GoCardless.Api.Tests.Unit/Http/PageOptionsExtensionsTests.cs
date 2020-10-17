using GoCardless.Api.Http;
using GoCardless.Api.Http.Serialisation;
using NUnit.Framework;
using System;
using System.Linq;

namespace GoCardless.Api.Tests.Unit.Http
{
    public class PageOptionsExtensionsTests
    {
        [Test]
        public void ReturnsEmptyDictionaryWhenNoPropertiesSupplied()
        {
            // given
            var subject = new FakePageOptions();

            // when
            var result = subject.ToReadOnlyDictionary();

            // then
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ExcludesNullProperties()
        {
            // given
            var subject = new FakePageOptions
            {
                Before = "before test",
                Limit = 5
            };

            // when
            var result = subject.ToReadOnlyDictionary();

            // then
            Assert.That(result.Keys.Count(), Is.EqualTo(2));
            Assert.That(result["before"], Is.EqualTo("before test"));
            Assert.That(result["limit"], Is.EqualTo(5));
        }

        [Test]
        public void IncludesAllSuppliedProperties()
        {
            // given
            var subject = new FakePageOptions
            {
                Before = "before test",
                After = "after test",
                Limit = 5
            };

            // when
            var result = subject.ToReadOnlyDictionary();

            // then
            Assert.That(result.Keys.Count(), Is.EqualTo(3));
            Assert.That(result["before"], Is.EqualTo("before test"));
            Assert.That(result["after"], Is.EqualTo("after test"));
            Assert.That(result["limit"], Is.EqualTo(5));
        }

        [Test]
        public void PreservesCaseOfValues()
        {
            // given
            var subject = new FakePageOptions
            {
                Before = "BEFORE teSt",
                After = "AFTER teSt"
            };

            // when
            var result = subject.ToReadOnlyDictionary();

            // then
            Assert.That(result["before"], Is.EqualTo("BEFORE teSt"));
            Assert.That(result["after"], Is.EqualTo("AFTER teSt"));
        }

        [Test]
        public void UsesQueryParamKeyWhenAttributeSupplied()
        {
            // given
            var expected = DateTimeOffset.UtcNow;
            var subject = new FakePageOptions
            {
                CreatedGreaterThanOrEqual = expected
            };

            // when
            var result = subject.ToReadOnlyDictionary();

            // then
            Assert.That(result["created_at[gte]"], Is.EqualTo(expected));
        }

        private class FakePageOptions : IPageOptions
        {
            public string Before { get; set; }

            public string After { get; set; }

            public int? Limit { get; set; }

            [QueryStringKey("created_at[gte]")]
            public DateTimeOffset? CreatedGreaterThanOrEqual { get; set; }
        }
    }
}