using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public interface IEventsClient
    {
        IPagerBuilder<GetEventsOptions, Event> BuildPager();
        Task<Response<Event>> ForIdAsync(string id);
        Task<PagedResponse<Event>> GetPageAsync();
        Task<PagedResponse<Event>> GetPageAsync(GetEventsOptions request);
    }
}