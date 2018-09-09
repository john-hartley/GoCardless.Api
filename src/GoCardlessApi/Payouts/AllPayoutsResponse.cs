using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.Payouts
{
    public class AllPayoutsResponse
    {
        public IEnumerable<Payout> Payouts { get; set; }
        public Meta Meta { get; set; }
    }
}