using GoCardlessApi.Http;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Http
{
    public class PagerTests
    {
        [Test]
        public void PagerIsNullThrows()
        {
            // given
            Func<FakePageOptions, Task<PagedResponse<string>>> source = null;
            var options = new FakePageOptions();

            // when
            TestDelegate test = () => new Pager<FakePageOptions, string>(source, options);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(source)));
        }

        [Test]
        public void OptionsIsNullThrows()
        {
            // given
            Func<FakePageOptions, Task<PagedResponse<string>>> source = _ => Task.FromResult(new PagedResponse<string>());
            FakePageOptions options = null;

            // when
            TestDelegate test = () => new Pager<FakePageOptions, string>(source, options);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public void PagingBeforeThrowsOnCancel()
        {
            // given
            var cancellationSource = new CancellationTokenSource();

            Func<FakePageOptions, Task<PagedResponse<string>>> source = _ =>
            {
                cancellationSource.Cancel();
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
            var options = new FakePageOptions();

            var subject = new Pager<FakePageOptions, string>(source, options);

            // when
            AsyncTestDelegate test = () => subject.AndGetAllBeforeAsync(cancellationSource.Token);

            // then
            Assert.That(test, Throws.InstanceOf<OperationCanceledException>());
        }

        [Test]
        public void PagingAfterThrowsOnCancel()
        {
            // given
            var cancellationSource = new CancellationTokenSource();

            Func<FakePageOptions, Task<PagedResponse<string>>> source = _ =>
            {
                cancellationSource.Cancel();
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
            var options = new FakePageOptions();

            var subject = new Pager<FakePageOptions, string>(source, options);

            // when
            AsyncTestDelegate test = () => subject.AndGetAllAfterAsync(cancellationSource.Token);

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
