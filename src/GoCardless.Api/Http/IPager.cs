using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoCardlessApi.Http
{
    public interface IPager<TRequest, TResource>
    {
        Task<IReadOnlyList<TResource>> AndGetAllBeforeAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TResource>> AndGetAllAfterAsync(CancellationToken cancellationToken = default);
    }
}