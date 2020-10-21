using GoCardlessApi.BankDetailsLookups;
using GoCardlessApi.CreditorBankAccounts;
using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.CustomerNotifications;
using GoCardlessApi.Events;
using GoCardlessApi.InstalmentSchedules;
using GoCardlessApi.MandateImportEntries;
using GoCardlessApi.MandateImports;
using GoCardlessApi.MandatePdfs;
using GoCardlessApi.Mandates;
using GoCardlessApi.Payments;
using GoCardlessApi.Payouts;
using GoCardlessApi.RedirectFlows;
using GoCardlessApi.Refunds;
using GoCardlessApi.Subscriptions;
using Newtonsoft.Json;
using Customer = GoCardlessApi.Customers.Customer;

namespace GoCardlessApi.Http
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

        [JsonProperty("customer_notifications")]
        private CustomerNotification CustomerNotification { set => Item = value as TResource; }

        [JsonProperty("customers")]
        private Customer Customer { set => Item = value as TResource; }

        [JsonProperty("events")]
        private Event Event { set => Item = value as TResource; }

        [JsonProperty("instalment_schedules")]
        private InstalmentSchedule InstalmentSchedule { set => Item = value as TResource; }

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

        [JsonProperty("redirect_flows")]
        private RedirectFlow RedirectFlow { set => Item = value as TResource; }

        [JsonProperty("refunds")]
        private Refund Refund { set => Item = value as TResource; }

        [JsonProperty("subscriptions")]
        private Subscription Subscription { set => Item = value as TResource; }
    }
}