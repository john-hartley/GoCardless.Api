using GoCardless.Api.Payouts;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration.Core.Paging
{
    public class PagerTests : IntegrationTest
    {
        [Test]
        public async Task ReturnsPagesIncludingAndBeforeInitialRequest()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);
            var lastId = (await subject.GetPageAsync()).Items.Last().Id;

            var initialRequest = new GetPayoutsRequest
            {
                Before = lastId,
                Limit = 1,
            };

            // when
            var result = await subject
                .BuildPager()
                .StartFrom(initialRequest)
                .AndGetAllBeforeAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }

        [Test]
        public async Task ReturnsPagesIncludingAndAfterInitialRequest()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);

            var initialRequest = new GetPayoutsRequest
            {
                Limit = 1,
            };

            // when
            var result = await subject
                .BuildPager()
                .StartFrom(initialRequest)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }

        [Test]
        public async Task ReturnsPagesIncludingAndAfterInitialRequestWhenCursorSpecified()
        {
            // given
            var subject = new PayoutsClient(_clientConfiguration);
            var firstId = (await subject.GetPageAsync()).Items.First().Id;

            var initialRequest = new GetPayoutsRequest
            {
                After = firstId,
                Limit = 1,
            };

            // when
            var result = await subject
                .BuildPager()
                .StartFrom(initialRequest)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}