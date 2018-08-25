using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class CreditorBankAccount
    {
        public string Id { get; set; }

        [JsonProperty("account_holder_name")]
        public string AccountHolderName { get; set; }

        [JsonProperty("account_number_ending")]
        public string AccountNumberEnding { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        public string Currency { get; set; }

        public bool Enabled { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public CreditorBankAccountLinks Links { get; set; }
    }
}