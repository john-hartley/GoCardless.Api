using GoCardless.Api.CreditorBankAccounts;
using GoCardless.Api.Creditors;
using GoCardless.Api.CustomerBankAccounts;
using GoCardless.Api.Events;
using GoCardless.Api.MandateImportEntries;
using GoCardless.Api.Mandates;
using GoCardless.Api.Payments;
using GoCardless.Api.PayoutItems;
using GoCardless.Api.Payouts;
using GoCardless.Api.Refunds;
using GoCardless.Api.Subscriptions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardless.Api.Core
{
    public class PagedResponse<TResource>
    {
        public IReadOnlyList<TResource> Items { get; set; }

        [JsonProperty("creditor_bank_accounts")]
        private IList<CreditorBankAccount> CreditorBankAccounts { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("creditors")]
        private IList<Creditor> Creditors { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("customer_bank_accounts")]
        private IList<CustomerBankAccount> CustomerBankAccounts { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("customers")]
        private IList<Customers.Customer> Customers { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("events")]
        private IList<Event> Events { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("mandate_import_entries")]
        private IList<MandateImportEntry> MandateImportEntries { set => Items = value as IReadOnlyList<TResource>; }
        
        [JsonProperty("mandates")]
        private IList<Mandate> Mandates { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("payments")]
        private IList<Payment> Payments { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("payouts")]
        private IList<Payout> Payouts { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("payout_items")]
        private IList<PayoutItem> PayoutItems { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("refunds")]
        private IList<Refund> Refunds { set => Items = value as IReadOnlyList<TResource>; }

        [JsonProperty("subscriptions")]
        private IList<Subscription> Subscriptions { set => Items = value as IReadOnlyList<TResource>; }

        public Meta Meta { get; set; }
    }
}