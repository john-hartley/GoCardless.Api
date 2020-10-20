using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoCardlessApi.Http
{
    public class Pager<TOptions, TResource> : IPager<TOptions, TResource>
        where TOptions : IPageOptions, ICloneable
    {
        private const int MaxItemsPerPage = 500;

        private readonly Func<TOptions, Task<PagedResponse<TResource>>> _source;
        private readonly TOptions _options;

        public Pager(Func<TOptions, Task<PagedResponse<TResource>>> source, TOptions options)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (options.Limit.HasValue && options.Limit.Value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(options.Limit));
            }

            _options = (TOptions)options.Clone();
        }

        public async Task<IReadOnlyList<TResource>> GetItemsBeforeAsync(CancellationToken cancellationToken = default)
        {
            var options = (TOptions)_options.Clone();
            var maxItems = options.Limit;

            if (options.Limit == null)
            {
                options.Limit = MaxItemsPerPage;
            }

            var results = new List<TResource>();
            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                var response = await _source(options).ConfigureAwait(false);
                results.AddRange(response.Items ?? Enumerable.Empty<TResource>());
                
                if (maxItems.HasValue && results.Count >= maxItems)
                {
                    return results.Take(maxItems.Value).ToList();
                }

                options.Before = response.Meta.Cursors.Before;
            } while (options.Before != null);

            return results;
        }

        public async Task<IReadOnlyList<TResource>> GetItemsAfterAsync(CancellationToken cancellationToken = default)
        {
            var options = (TOptions)_options.Clone();
            var maxItems = options.Limit;

            if (options.Limit == null)
            {
                options.Limit = MaxItemsPerPage;
            }

            var results = new List<TResource>();
            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                var response = await _source(options).ConfigureAwait(false);
                results.AddRange(response.Items ?? Enumerable.Empty<TResource>());

                if (maxItems.HasValue && results.Count >= maxItems)
                {
                    return results.Take(maxItems.Value).ToList();
                }

                options.After = response.Meta.Cursors.After;
            } while (options.After != null);

            return results;
        }
    }
}