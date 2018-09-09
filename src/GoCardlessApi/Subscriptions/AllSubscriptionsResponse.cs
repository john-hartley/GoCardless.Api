using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.Subscriptions
{
    public class AllSubscriptionsResponse
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public Meta Meta { get; set; }
    }
}