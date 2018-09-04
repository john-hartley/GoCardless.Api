using GoCardlessApi.Core;
using System.Collections.Generic;

namespace GoCardlessApi.Subscriptions
{
    public class AllSubscriptionsResponse
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public Meta Meta { get; set; }
    }
}