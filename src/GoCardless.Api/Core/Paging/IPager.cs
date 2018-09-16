﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Paging
{
    public interface IPager<TRequest, TResource>
    {
        Task<IReadOnlyList<TResource>> AndGetAllBeforeAsync();
        Task<IReadOnlyList<TResource>> AndGetAllAfterAsync();
    }
}