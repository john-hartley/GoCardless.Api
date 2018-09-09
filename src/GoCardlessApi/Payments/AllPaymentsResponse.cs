using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.Payments
{
    public class AllPaymentsResponse
    {
        public IEnumerable<Payment> Payments { get; set; }
        public Meta Meta { get; set; }
    }
}