using GoCardless.Api.Core.Http;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit.Core.Http
{
    public class PagerTests
    {
        [Test]
        public void PagerIsNullThrows()
        {
            // given
            Func<FakePageOptions, Task<PagedResponse<string>>> pager = null;

            // when
            TestDelegate test = () => new Pager<FakePageOptions, string>(pager);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(pager)));
        }

        [Test]
        public void InitialRequestIsNullThrows()
        {
            // given
            Func<FakePageOptions, Task<PagedResponse<string>>> pager = _ => Task.FromResult(new PagedResponse<string>());
            var subject = new Pager<FakePageOptions, string>(pager);

            FakePageOptions initialRequest = null;

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
            Func<FakePageOptions, Task<PagedResponse<string>>> pager = _ => Task.FromResult(new PagedResponse<string>());
            var subject = new Pager<FakePageOptions, string>(pager);

            // when
            AsyncTestDelegate test = () => subject.AndGetAllBeforeAsync();

            // then
            var ex = Assert.ThrowsAsync<InvalidOperationException>(test);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void PagingBeforeThrowsOnCancel()
        {
            // given
            var source = new CancellationTokenSource();

            Func<FakePageOptions, Task<PagedResponse<string>>> pager = _ =>
            {
                source.Cancel();
                return Task.FromResult(new PagedResponse<string>
                {
                    Meta = new Meta
                    {
                        Cursors = new Cursors
                        {
                            Before = "1"
                        },
                        Limit = 1
                    }
                });
            };

            var subject = new Pager<FakePageOptions, string>(pager);

            subject.StartFrom(new FakePageOptions());

            // when
            AsyncTestDelegate test = () => subject.AndGetAllBeforeAsync(source.Token);

            // then
            Assert.That(test, Throws.InstanceOf<OperationCanceledException>());
        }

        [Test]
        public void InitialRequestIsNullWhenPagingAfterThrows()
        {
            // given
            Func<FakePageOptions, Task<PagedResponse<string>>> pager = _ => Task.FromResult(new PagedResponse<string>());
            var subject = new Pager<FakePageOptions, string>(pager);

            // when
            AsyncTestDelegate test = () => subject.AndGetAllAfterAsync();

            // then
            var ex = Assert.ThrowsAsync<InvalidOperationException>(test);
            Assert.That(ex.Message, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void PagingAfterThrowsOnCancel()
        {
            // given
            var source = new CancellationTokenSource();

            Func<FakePageOptions, Task<PagedResponse<string>>> pager = _ =>
            {
                source.Cancel();
                return Task.FromResult(new PagedResponse<string>
                {
                    Meta = new Meta
                    {
                        Cursors = new Cursors
                        {
                            After = "1"
                        },
                        Limit = 1
                    }
                });
            };

            var subject = new Pager<FakePageOptions, string>(pager);

            subject.StartFrom(new FakePageOptions());

            // when
            AsyncTestDelegate test = () => subject.AndGetAllAfterAsync(source.Token);

            // then
            Assert.That(test, Throws.InstanceOf<OperationCanceledException>());
        }
    }

    internal class FakePageOptions : IPageOptions, ICloneable
    {
        public string After { get; set; }
        public string Before { get; set; }
        public int? Limit { get; set; } = 1;

        public object Clone()
        {
            return new FakePageOptions
            {
                After = After,
                Before = Before,
                Limit = Limit,
            };
        }
    }
}
