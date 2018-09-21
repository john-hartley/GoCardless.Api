using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Http
{
    public interface IPager<TRequest, TResource>
    {
        Task<IReadOnlyList<TResource>> AndGetAllBeforeAsync();
        Task<IReadOnlyList<TResource>> AndGetAllAfterAsync();
    }
}