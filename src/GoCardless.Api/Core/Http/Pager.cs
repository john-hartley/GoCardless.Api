using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public class Pager<TOptions, TResource> 
        : IPagerBuilder<TOptions, TResource>, IPager<TOptions, TResource>
        where TOptions : IPageOptions, ICloneable
    {
        private readonly Func<TOptions, Task<PagedResponse<TResource>>> _pager;

        private TOptions _options;

        public Pager(Func<TOptions, Task<PagedResponse<TResource>>> pager)
        {
            _pager = pager ?? throw new ArgumentNullException(nameof(pager));
        }

        public IPager<TOptions, TResource> StartFrom(TOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = (TOptions)options.Clone();
            return this;
        }

        public async Task<IReadOnlyList<TResource>> AndGetAllBeforeAsync(CancellationToken cancellationToken = default)
        {
            if (_options == null)
            {
                throw new InvalidOperationException($"{nameof(_options)} was null when attempting paging. You must call the {nameof(Pager<TOptions, TResource>.StartFrom)} method first.");
            }

            var results = new List<TResource>();
            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                var response = await _pager(_options).ConfigureAwait(false);
                results.AddRange(response.Items ?? Enumerable.Empty<TResource>());

                _options.Before = response.Meta.Cursors.Before;
            } while (_options.Before != null);

            return results;
        }

        public async Task<IReadOnlyList<TResource>> AndGetAllAfterAsync(CancellationToken cancellationToken = default)
        {
            if (_options == null)
            {
                throw new InvalidOperationException($"{nameof(_options)} was null when attempting paging. You must call the {nameof(Pager<TOptions, TResource>.StartFrom)} method first.");
            }

            var results = new List<TResource>();
            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                var response = await _pager(_options).ConfigureAwait(false);
                results.AddRange(response.Items ?? Enumerable.Empty<TResource>());

                _options.After = response.Meta.Cursors.After;
            } while (_options.After != null);

            return results;
        }
    }
}