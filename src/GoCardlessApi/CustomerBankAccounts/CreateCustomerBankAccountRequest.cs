﻿using System.Collections.Generic;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class CreateCustomerBankAccountRequest
    {
        public CreateCustomerBankAccountRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        public string AccountHolderName { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public string BranchCode { get; set; }

        public string CountryCode { get; set; }

        public string Currency { get; set; }

        public string Iban { get; set; }

        public CustomerBankAccountLinks Links { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}