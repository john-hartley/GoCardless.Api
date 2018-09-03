using System.Collections.Generic;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class CreateCreditorBankAccountRequest
    {
        public string AccountHolderName { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public string BranchCode { get; set; }

        public string CountryCode { get; set; }

        public string Currency { get; set; }

        public string Iban { get; set; }

        public CreditorBankAccountLinks Links { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public bool SetAsDefaultPayoutAccount { get; set; }
    }
}