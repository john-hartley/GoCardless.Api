using System.Collections.Generic;

namespace GoCardlessApi
{
    public class SubscriptionsResponse
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}