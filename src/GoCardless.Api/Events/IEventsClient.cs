using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public interface IEventsClient
    {
        Task<AllEventsResponse> AllAsync();
        Task<AllEventsResponse> AllAsync(AllEventsRequest request);
        Task<EventResponse> ForIdAsync(string eventId);
    }
}