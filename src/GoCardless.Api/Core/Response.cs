using GoCardless.Api.BankDetailsLookups;
using GoCardless.Api.CreditorBankAccounts;
using GoCardless.Api.Creditors;
using GoCardless.Api.CustomerBankAccounts;
using GoCardless.Api.Events;
using GoCardless.Api.MandateImportEntries;
using GoCardless.Api.MandateImports;
using GoCardless.Api.MandatePdfs;
using GoCardless.Api.Mandates;
using GoCardless.Api.Payments;
using GoCardless.Api.Payouts;
using Newtonsoft.Json;
using Customer = GoCardless.Api.Customers.Customer;

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

        [JsonProperty("customers")]
        private Customer Customer { set => Item = value as TResource; }

        [JsonProperty("events")]
        private Event Event { set => Item = value as TResource; }

        [JsonProperty("mandate_import_entries")]
        private MandateImportEntry MandateImportEntry { set => Item = value as TResource; }

        [JsonProperty("mandate_imports")]
        private MandateImport MandateImport { set => Item = value as TResource; }

        [JsonProperty("mandate_pdfs")]
        private MandatePdf MandatePdf { set => Item = value as TResource; }

        [JsonProperty("mandates")]
        private Mandate Mandate { set => Item = value as TResource; }

        [JsonProperty("payments")]
        private Payment Payment { set => Item = value as TResource; }

        [JsonProperty("payouts")]
        private Payout Payout { set => Item = value as TResource; }
    }
}