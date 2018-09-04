using GoCardlessApi.Core;
using System.Collections.Generic;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class AllCustomerBankAccountsResponse
    {
        public IEnumerable<CustomerBankAccount> CustomerBankAccounts { get; set; }
        public Meta Meta { get; set; }
    }
}