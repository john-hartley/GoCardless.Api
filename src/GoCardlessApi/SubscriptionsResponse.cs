using System;
using System.Collections.Generic;
using System.Text;

namespace GoCardlessApi
{
    public class SubscriptionsResponse
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}