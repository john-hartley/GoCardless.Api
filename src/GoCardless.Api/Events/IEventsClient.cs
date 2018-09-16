﻿using GoCardless.Api.Core;
using GoCardless.Api.Core.Paging;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public interface IEventsClient
    {
        IPagerBuilder<GetEventsRequest, Event> BuildPager();
        Task<Response<Event>> ForIdAsync(string eventId);
        Task<PagedResponse<Event>> GetPageAsync();
        Task<PagedResponse<Event>> GetPageAsync(GetEventsRequest request);
    }
}