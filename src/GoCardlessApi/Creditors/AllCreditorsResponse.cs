using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.Creditors
{
    public class AllCreditorsResponse
    {
        public IEnumerable<Creditor> Creditors { get; set; }
        public Meta Meta { get; set; }
    }
}