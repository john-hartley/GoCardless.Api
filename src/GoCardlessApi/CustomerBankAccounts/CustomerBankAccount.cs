using System.Collections.Generic;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class CustomerBankAccount
    {
        public string Id { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumberEnding { get; set; }
        public string BankName { get; set; }
        public string CountryCode { get; set; }
        public string Currency { get; set; }
        public bool Enabled { get; set; }
        public CustomerBankAccountLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}