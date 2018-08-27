using System.Collections.Generic;

namespace GoCardlessApi.Payouts
{
    public class AllPayoutsResponse
    {
        public IEnumerable<Payout> Payouts { get; set; }
    }
}