using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class AllCreditorBankAccountsResponse
    {
        [JsonProperty("creditor_bank_accounts")]
        public IEnumerable<CreditorBankAccount> CreditorBankAccounts { get; set; }
    }
}