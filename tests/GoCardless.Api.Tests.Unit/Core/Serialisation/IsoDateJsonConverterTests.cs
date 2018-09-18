using GoCardless.Api.Core.Serialisation;
using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace GoCardless.Api.Tests.Unit.Core.Serialisation
{
    public class IsoDateJsonConverterTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void DateFormatIsNullOrWhiteSpaceThrows(string dateFormat)
        {
            // given
            // when
            TestDelegate test = () => new IsoDateJsonConverter(dateFormat);

            // then
            var ex = Assert.Throws<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(dateFormat)));
        }

        [Test]
        public void SerialisesDateToIsoFormat()
        {
            // given
            var fake = new FakeDateModel
            {
                NullableTest = new DateTime(2018, 9, 18, 2, 30, 5),
                Test = new DateTime(2018, 9, 18, 2, 30, 5)
            };

            var expected = "{\"NullableTest\":\"2018-09-18\",\"Test\":\"2018-09-18\"}";

            // when
            var result = JsonConvert.SerializeObject(fake);

            // then
            Assert.That(result, Is.EqualTo(expected));
        }
    }

    internal class FakeDateModel
    {
        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime? NullableTest { get; set; }

        [JsonConverter(typeof(IsoDateJsonConverter), DateFormat.IsoDateFormat)]
        public DateTime Test { get; set; }
    }
}