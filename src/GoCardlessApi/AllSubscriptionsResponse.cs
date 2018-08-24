using System.Collections.Generic;

namespace GoCardlessApi
{
    public class AllSubscriptionsResponse
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}