﻿using GoCardless.Api.BankDetailsLookups;
using GoCardless.Api.CreditorBankAccounts;
using GoCardless.Api.Creditors;
using GoCardless.Api.CustomerBankAccounts;
using Newtonsoft.Json;

namespace GoCardless.Api.Core
{
    public class Response<TResource> where TResource : class
    {
        public TResource Item { get; set; }

        [JsonProperty("bank_details_lookups")]
        private BankDetailsLookup BankDetailsLookup { set => Item = value as TResource; }

        [JsonProperty("creditor_bank_accounts")]
        private CreditorBankAccount CreditorBankAccount { set => Item = value as TResource; }

        [JsonProperty("creditors")]
        private Creditor Creditor { set => Item = value as TResource; }

        [JsonProperty("customer_bank_accounts")]
        private CustomerBankAccount CustomerBankAccount { set => Item = value as TResource; }
    }
}