using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class CreateCreditorBankAccountOptions
    {
        public CreateCreditorBankAccountOptions()
        {
            IdempotencyKey = Guid.NewGuid().ToString();
        }

        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string CountryCode { get; set; }
        public string Currency { get; set; }
        public string Iban { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public CreditorBankAccountLinks Links { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public bool SetAsDefaultPayoutAccount { get; set; }
    }
}