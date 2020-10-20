using GoCardlessApi.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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

        [Test]
        public async Task BeforeLimitsItemsReturned()
        {
            // given
            var responses = new Queue<PagedResponse<string>>();

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string> { "1", "2", "3", "4", "5" },
                Meta = new Meta
                {
                    Cursors = new Cursors
                    {
                        Before = "1"
                    },
                    Limit = 5
                }
            });

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string>(),
                Meta = new Meta
                {
                    Cursors = new Cursors(),
                    Limit = 5
                }
            });

            Func<FakePageOptions, Task<PagedResponse<string>>> source = _ =>
            {
                return Task.FromResult(responses.Dequeue());
            };

            var options = new FakePageOptions
            {
                Limit = 3
            };

            var subject = new Pager<FakePageOptions, string>(source, options);

            // when
            var results = await subject.AndGetAllBeforeAsync();

            // then
            Assert.That(results.Count, Is.EqualTo(options.Limit));
        }

        [Test]
        public async Task BeforeLimitsItemsReturnedAcrossPages()
        {
            // given
            var responses = new Queue<PagedResponse<string>>();

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string> { "4", "5" },
                Meta = new Meta
                {
                    Cursors = new Cursors
                    {
                        Before = "4"
                    },
                    Limit = 2
                }
            });

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string> { "2", "3" },
                Meta = new Meta
                {
                    Cursors = new Cursors
                    {
                        Before = "2"
                    },
                    Limit = 2
                }
            });

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string> { "1" },
                Meta = new Meta
                {
                    Cursors = new Cursors(),
                    Limit = 2
                }
            });

            Func<FakePageOptions, Task<PagedResponse<string>>> source = _ =>
            {
                return Task.FromResult(responses.Dequeue());
            };

            var options = new FakePageOptions
            {
                Limit = 4
            };

            var subject = new Pager<FakePageOptions, string>(source, options);

            // when
            var results = await subject.AndGetAllBeforeAsync();

            // then
            Assert.That(results.Count, Is.EqualTo(options.Limit));
        }

        [Test]
        public async Task AfterLimitsItemsReturned()
        {
            // given
            var responses = new Queue<PagedResponse<string>>();

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string> { "1", "2", "3", "4", "5" },
                Meta = new Meta
                {
                    Cursors = new Cursors
                    {
                        After = "5"
                    },
                    Limit = 5
                }
            });

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string>(),
                Meta = new Meta
                {
                    Cursors = new Cursors(),
                    Limit = 5
                }
            });

            Func<FakePageOptions, Task<PagedResponse<string>>> source = _ =>
            {
                return Task.FromResult(responses.Dequeue());
            };

            var options = new FakePageOptions
            {
                Limit = 3
            };

            var subject = new Pager<FakePageOptions, string>(source, options);

            // when
            var results = await subject.AndGetAllAfterAsync();

            // then
            Assert.That(results.Count, Is.EqualTo(options.Limit));
        }

        [Test]
        public async Task AfterLimitsItemsReturnedAcrossPages()
        {
            // given
            var responses = new Queue<PagedResponse<string>>();

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string> { "1", "2" },
                Meta = new Meta
                {
                    Cursors = new Cursors
                    {
                        After = "2"
                    },
                    Limit = 2
                }
            });

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string> { "3", "4" },
                Meta = new Meta
                {
                    Cursors = new Cursors
                    {
                        After = "4"
                    },
                    Limit = 2
                }
            });

            responses.Enqueue(new PagedResponse<string>
            {
                Items = new List<string> { "5" },
                Meta = new Meta
                {
                    Cursors = new Cursors(),
                    Limit = 2
                }
            });

            Func<FakePageOptions, Task<PagedResponse<string>>> source = _ =>
            {
                return Task.FromResult(responses.Dequeue());
            };

            var options = new FakePageOptions
            {
                Limit = 4
            };

            var subject = new Pager<FakePageOptions, string>(source, options);

            // when
            var results = await subject.AndGetAllAfterAsync();

            // then
            Assert.That(results.Count, Is.EqualTo(options.Limit));
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
