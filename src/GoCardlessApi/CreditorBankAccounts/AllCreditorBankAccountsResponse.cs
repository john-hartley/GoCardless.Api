using GoCardlessApi.Core;
using System.Collections.Generic;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class AllCreditorBankAccountsResponse
    {
        public IEnumerable<CreditorBankAccount> CreditorBankAccounts { get; set; }

        public Meta Meta { get; set; }
    }
}