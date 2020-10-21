using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoCardlessApi.Http
{
    public interface IPager<TRequest, TResource>
    {
        Task<IReadOnlyList<TResource>> GetItemsBeforeAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TResource>> GetItemsAfterAsync(CancellationToken cancellationToken = default);
    }
}