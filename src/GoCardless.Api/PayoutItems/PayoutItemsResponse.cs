using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.PayoutItems
{
    public class PayoutItemsResponse
    {
        public IEnumerable<PayoutItem> PayoutItems { get; set; }
        public Meta Meta { get; set; }
    }
}