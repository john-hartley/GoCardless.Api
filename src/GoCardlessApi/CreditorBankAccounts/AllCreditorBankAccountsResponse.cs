using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class AllCreditorBankAccountsResponse
    {
        public IEnumerable<CreditorBankAccount> CreditorBankAccounts { get; set; }
        public Meta Meta { get; set; }
    }
}