using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoCardless.Api.Http
{
    public class Pager<TOptions, TResource> : IPager<TOptions, TResource>
        where TOptions : IPageOptions, ICloneable
    {
        private readonly Func<TOptions, Task<PagedResponse<TResource>>> _source;
        private readonly TOptions _options;

        public Pager(Func<TOptions, Task<PagedResponse<TResource>>> source, TOptions options)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = (TOptions)options.Clone();
        }

        public async Task<IReadOnlyList<TResource>> AndGetAllBeforeAsync(CancellationToken cancellationToken = default)
        {
            var results = new List<TResource>();
            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                var response = await _source(_options).ConfigureAwait(false);
                results.AddRange(response.Items ?? Enumerable.Empty<TResource>());

                _options.Before = response.Meta.Cursors.Before;
            } while (_options.Before != null);

            return results;
        }

        public async Task<IReadOnlyList<TResource>> AndGetAllAfterAsync(CancellationToken cancellationToken = default)
        {
            var results = new List<TResource>();
            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                var response = await _source(_options).ConfigureAwait(false);
                results.AddRange(response.Items ?? Enumerable.Empty<TResource>());

                _options.After = response.Meta.Cursors.After;
            } while (_options.After != null);

            return results;
        }
    }
}