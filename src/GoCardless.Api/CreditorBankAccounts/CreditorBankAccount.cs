using System.Collections.Generic;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class CreditorBankAccount
    {
        public string Id { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumberEnding { get; set; }
        public string BankName { get; set; }
        public string CountryCode { get; set; }
        public string Currency { get; set; }
        public bool Enabled { get; set; }
        public CreditorBankAccountLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}