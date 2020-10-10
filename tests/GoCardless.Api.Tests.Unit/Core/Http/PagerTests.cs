using GoCardless.Api.Core.Http;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit.Core.Http
{
    public class PagerTests
    {
        [Test]
        public void PagerIsNullThrows()
        {
            // given
            Func<FakePageRequest, Task<PagedResponse<string>>> pager = null;

            // when
            TestDelegate test = () => new Pager<FakePageRequest, string>(pager);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(pager)));
        }

        [Test]
        public void InitialRequestIsNullThrows()
        {
            // given
            Func<FakePageRequest, Task<PagedResponse<string>>> pager = _ => Task.FromResult(new PagedResponse<string>());
            var subject = new Pager<FakePageRequest, string>(pager);

            FakePageRequest initialRequest = null;

            // when
            TestDelegate test = () => subject.StartFrom(initialRequest);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(initialRequest)));
        }

        [Test]
        public void InitialRequestIsNullWhenPagingBeforeThrows()
        {
            // given
            Func<FakePageRequest, Task<PagedResponse<string>>> pager = _ => Task.FromResult(new PagedResponse<string>());
            var subject = new Pager<FakePageRequest, string>(pager);

            // when
            AsyncTestDelegate test = () => subject.AndGetAllBeforeAsync();

            // then
            var ex = Assert.ThrowsAsync<InvalidOperationException>(test);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void InitialRequestIsNullWhenPagingAfterThrows()
        {
            // given
            Func<FakePageRequest, Task<PagedResponse<string>>> pager = _ => Task.FromResult(new PagedResponse<string>());
            var subject = new Pager<FakePageRequest, string>(pager);

            // when
            AsyncTestDelegate test = () => subject.AndGetAllAfterAsync();

            // then
            var ex = Assert.ThrowsAsync<InvalidOperationException>(test);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
        }
    }

    internal class FakePageRequest : IPageOptions, ICloneable
    {
        public string After { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Before { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
