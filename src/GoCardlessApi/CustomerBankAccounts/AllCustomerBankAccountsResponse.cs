using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class AllCustomerBankAccountsResponse
    {
        public IEnumerable<CustomerBankAccount> CustomerBankAccounts { get; set; }
        public Meta Meta { get; set; }
    }
}