using GoCardless.Api.Core.Paging;
using System;

namespace GoCardless.Api.PayoutItems
{
    public class GetPayoutItemsRequest : IPageRequest, ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        public string Before { get; set; }
        public string After { get; set; }
        public int? Limit { get; set; }
        public string Payout { get; set; }
    }
}