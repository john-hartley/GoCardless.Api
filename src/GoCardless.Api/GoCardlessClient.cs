using GoCardlessApi.BankDetailsLookups;
using GoCardlessApi.CreditorBankAccounts;
using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.CustomerNotifications;
using GoCardlessApi.Customers;
using GoCardlessApi.Events;
using GoCardlessApi.MandateImportEntries;
using GoCardlessApi.MandateImports;
using GoCardlessApi.MandatePdfs;
using GoCardlessApi.Mandates;
using GoCardlessApi.Payments;
using GoCardlessApi.PayoutItems;
using GoCardlessApi.Payouts;
using GoCardlessApi.RedirectFlows;
using GoCardlessApi.Refunds;
using GoCardlessApi.Subscriptions;
using System;

namespace GoCardlessApi
{
    public class GoCardlessClient : IGoCardlessClient
    {
        public GoCardlessClient(GoCardlessConfiguration configuration)
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