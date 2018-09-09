using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.Refunds
{
    public class AllRefundsResponse
    {
        public IEnumerable<Refund> Refunds { get; set; }
        public Meta Meta { get; set; }
    }
}