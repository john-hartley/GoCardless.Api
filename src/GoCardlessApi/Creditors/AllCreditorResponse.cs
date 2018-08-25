using System.Collections.Generic;

namespace GoCardlessApi.Creditors
{
    public class AllCreditorResponse
    {
        public IEnumerable<Creditor> Creditors { get; set; }
    }
}