﻿using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public interface IEventsClient
    {
        Task<PagedResponse<Event>> AllAsync();
        Task<PagedResponse<Event>> AllAsync(AllEventsRequest request);
        Task<Response<Event>> ForIdAsync(string eventId);
    }
}