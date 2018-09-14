using Newtonsoft.Json;

namespace GoCardless.Api.Events
{
    public class EventResponse
    {
        [JsonProperty("events")]
        public Event Event { get; set; }
    }
}