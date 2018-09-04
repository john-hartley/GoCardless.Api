using GoCardlessApi.Core;
using System.Collections.Generic;

namespace GoCardlessApi.Creditors
{
    public class AllCreditorsResponse
    {
        public IEnumerable<Creditor> Creditors { get; set; }
        public Meta Meta { get; set; }
    }
}