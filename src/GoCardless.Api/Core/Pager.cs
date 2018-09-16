using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Core
{
    public class Pager<TRequest, TResource> : IPagerBuilder<TRequest, TResource>, IPager<TRequest, TResource>
        where TRequest : IPageRequest, ICloneable
    {
        private readonly Func<TRequest, Task<PagedResponse<TResource>>> _pager;

        private TRequest _initialRequest;

        public Pager(Func<TRequest, Task<PagedResponse<TResource>>> pager)
        {
            _pager = pager ?? throw new ArgumentNullException(nameof(pager));
        }

        public IPager<TRequest, TResource> StartFrom(TRequest initialRequest)
        {
            if (initialRequest == null)
            {
                throw new ArgumentNullException(nameof(initialRequest));
            }

            _initialRequest = (TRequest)initialRequest.Clone();
            return this;
        }

        public async Task<IReadOnlyList<TResource>> AndGetAllBeforeAsync()
        {
            if (_initialRequest == null)
            {
                throw new InvalidOperationException($"{nameof(_initialRequest)} was null when attempting paging. You must call the {nameof(Pager<TRequest, TResource>.StartFrom)} method first.");
            }

            var results = new List<TResource>();
            do
            {
                var response = await _pager(_initialRequest).ConfigureAwait(false);
                results.AddRange(response.Items ?? Enumerable.Empty<TResource>());

                _initialRequest.Before = response.Meta.Cursors.Before;
            } while (_initialRequest.Before != null);

            return results;
        }

        public async Task<IReadOnlyList<TResource>> AndGetAllAfterAsync()
        {
            if (_initialRequest == null)
            {
                throw new InvalidOperationException($"{nameof(_initialRequest)} was null when attempting paging. You must call the {nameof(Pager<TRequest, TResource>.StartFrom)} method first.");
            }

            var results = new List<TResource>();
            do
            {
                var response = await _pager(_initialRequest).ConfigureAwait(false);
                results.AddRange(response.Items ?? Enumerable.Empty<TResource>());

                _initialRequest.After = response.Meta.Cursors.After;
            } while (_initialRequest.After != null);

            return results;
        }
    }
}