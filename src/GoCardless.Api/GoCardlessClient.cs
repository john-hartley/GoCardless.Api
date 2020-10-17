using GoCardless.Api.BankDetailsLookups;
using GoCardless.Api.Core.Http;
using GoCardless.Api.CreditorBankAccounts;
using GoCardless.Api.Creditors;
using GoCardless.Api.CustomerBankAccounts;
using GoCardless.Api.CustomerNotifications;
using GoCardless.Api.Customers;
using GoCardless.Api.Events;
using GoCardless.Api.MandateImportEntries;
using GoCardless.Api.MandateImports;
using GoCardless.Api.MandatePdfs;
using GoCardless.Api.Mandates;
using GoCardless.Api.Payments;
using GoCardless.Api.PayoutItems;
using GoCardless.Api.Payouts;
using GoCardless.Api.RedirectFlows;
using GoCardless.Api.Refunds;
using GoCardless.Api.Subscriptions;
using System;

namespace GoCardless.Api
{
    public class GoCardlessClient : IGoCardlessClient
    {
        public GoCardlessClient(ApiClientConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            BankDetailsLookups = new BankDetailsLookupsClient(configuration);
            CreditorBankAccounts = new CreditorBankAccountsClient(configuration);
            Creditors = new CreditorsClient(configuration);
            CustomerBankAccounts = new CustomerBankAccountsClient(configuration);
            CustomerNotifications = new CustomerNotificationsClient(configuration);
            Customers = new CustomersClient(configuration);
            Events = new EventsClient(configuration);
            MandateImportEntries = new MandateImportEntriesClient(configuration);
            MandateImports = new MandateImportsClient(configuration);
            MandatePdfs = new MandatePdfsClient(configuration);
            Mandates = new MandatesClient(configuration);
            Payments = new PaymentsClient(configuration);
            PayoutItems = new PayoutItemsClient(configuration);
            Payouts = new PayoutsClient(configuration);
            RedirectFlows = new RedirectFlowsClient(configuration);
            Refunds = new RefundsClient(configuration);
            Subscriptions = new SubscriptionsClient(configuration);
        }

        public IBankDetailsLookupsClient BankDetailsLookups { get; }
        public ICreditorBankAccountsClient CreditorBankAccounts { get; }
        public ICreditorsClient Creditors { get; }
        public ICustomerBankAccountsClient CustomerBankAccounts { get; }
        public ICustomerNotificationsClient CustomerNotifications { get; }
        public ICustomersClient Customers { get; }
        public IEventsClient Events { get; }
        public IMandateImportEntriesClient MandateImportEntries { get; }
        public IMandateImportsClient MandateImports { get; }
        public IMandatePdfsClient MandatePdfs { get; }
        public IMandatesClient Mandates { get; }
        public IPaymentsClient Payments { get; }
        public IPayoutItemsClient PayoutItems { get; }
        public IPayoutsClient Payouts { get; }
        public IRedirectFlowsClient RedirectFlows { get; }
        public IRefundsClient Refunds { get; }
        public ISubscriptionsClient Subscriptions { get; }
    }
}