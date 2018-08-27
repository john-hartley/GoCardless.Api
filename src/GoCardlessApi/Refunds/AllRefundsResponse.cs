using System;
using System.Collections.Generic;
using System.Text;

namespace GoCardlessApi.Refunds
{
    public class AllRefundsResponse
    {
        public IEnumerable<Refund> Refunds { get; set; }
    }
}