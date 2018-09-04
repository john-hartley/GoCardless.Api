using GoCardlessApi.Core;
using System.Collections.Generic;

namespace GoCardlessApi.Customers
{
    public class AllCustomersResponse
    {
        public IEnumerable<Customer> Customers { get; set; }
        public Meta Meta { get; set; }
    }
}