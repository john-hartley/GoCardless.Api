using GoCardless.Api.BankDetailsLookups;
using GoCardless.Api.Core.Configuration;
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

namespace GoCardless.Api
{
    public class GoCardlessClient
    {
        private readonly ClientConfiguration _configuration;

        public GoCardlessClient(ClientConfiguration configuration)
        {
            _configuration = configuration;
            var apiClient = new ApiClient(_configuration);

            BankDetailsLookups = new BankDetailsLookupsClient(apiClient);
            CreditorBankAccounts = new CreditorBankAccountsClient(apiClient);
            Creditors = new CreditorsClient(apiClient);
            CustomerBankAccounts = new CustomerBankAccountsClient(apiClient);
            CustomerNotifications = new CustomerNotificationsClient(apiClient);
            Customers = new CustomersClient(apiClient);
            Events = new EventsClient(apiClient);
            MandateImportEntries = new MandateImportEntriesClient(apiClient);
            MandateImports = new MandateImportsClient(apiClient);
            MandatePdfs = new MandatePdfsClient(apiClient);
            Mandates = new MandatesClient(apiClient);
            Payments = new PaymentsClient(apiClient);
            PayoutItems = new PayoutItemsClient(apiClient);
            Payouts = new PayoutsClient(apiClient);
            RedirectFlows = new RedirectFlowsClient(apiClient);
            Refunds = new RefundsClient(apiClient);
            Subscriptions = new SubscriptionsClient(apiClient);
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