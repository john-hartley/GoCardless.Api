using GoCardlessApi.Core;
using System.Collections.Generic;

namespace GoCardlessApi.PayoutItems
{
    public class PayoutItemsResponse
    {
        public IEnumerable<PayoutItem> PayoutItems { get; set; }
        public Meta Meta { get; set; }
    }
}