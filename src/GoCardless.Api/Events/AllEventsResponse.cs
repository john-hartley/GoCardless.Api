using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.Events
{
    public class AllEventsResponse
    {
        public IEnumerable<Event> Events { get; set; }
        public Meta Meta { get; set; }
    }
}