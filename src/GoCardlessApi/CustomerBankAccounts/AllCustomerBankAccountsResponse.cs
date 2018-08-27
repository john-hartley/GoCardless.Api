using GoCardlessApi.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class AllCustomerBankAccountsResponse
    {
        [JsonProperty("customer_bank_accounts")]
        public IEnumerable<CustomerBankAccount> CustomerBankAccounts { get; set; }

        public Meta Meta { get; set; }
    }
}