using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.Customers
{
    public class AllCustomersResponse
    {
        public IEnumerable<Customer> Customers { get; set; }
        public Meta Meta { get; set; }
    }
}