using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public interface IEventsClient
    {
        Task<PagedResponse<Event>> AllAsync();
        Task<PagedResponse<Event>> AllAsync(AllEventsRequest request);
        Task<EventResponse> ForIdAsync(string eventId);
    }
}