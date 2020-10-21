using GoCardlessApi.Http;
using System;

namespace GoCardlessApi.PayoutItems
{
    public class GetPayoutItemsOptions : IPageOptions, ICloneable
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