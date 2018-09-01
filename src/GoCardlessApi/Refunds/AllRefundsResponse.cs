using System.Collections.Generic;

namespace GoCardlessApi.Refunds
{
    public class AllRefundsResponse
    {
        public IEnumerable<Refund> Refunds { get; set; }
    }
}