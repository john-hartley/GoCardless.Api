using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Events
{
    public interface IEventsClient : IPageable<GetEventsOptions, Event>
    {
        Task<Response<Event>> ForIdAsync(string id);
        Task<PagedResponse<Event>> GetPageAsync();
        Task<PagedResponse<Event>> GetPageAsync(GetEventsOptions options);
    }
}