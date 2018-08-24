using System.Collections.Generic;

namespace GoCardlessApi
{
    public class AllCreditorResponse
    {
        public IEnumerable<Creditor> Creditors { get; set; }
    }
}