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
        private readonly IApiClient _apiClient;

        public GoCardlessClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public GoCardlessClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);

            BankDetailsLookups = new BankDetailsLookupsClient(_apiClient);
            CreditorBankAccounts = new CreditorBankAccountsClient(_apiClient);
            Creditors = new CreditorsClient(_apiClient);
            CustomerBankAccounts = new CustomerBankAccountsClient(_apiClient);
            CustomerNotifications = new CustomerNotificationsClient(_apiClient);
            Customers = new CustomersClient(_apiClient);
            Events = new EventsClient(_apiClient);
            MandateImportEntries = new MandateImportEntriesClient(_apiClient);
            MandateImports = new MandateImportsClient(_apiClient);
            MandatePdfs = new MandatePdfsClient(_apiClient);
            Mandates = new MandatesClient(_apiClient);
            Payments = new PaymentsClient(_apiClient);
            PayoutItems = new PayoutItemsClient(_apiClient);
            Payouts = new PayoutsClient(_apiClient);
            RedirectFlows = new RedirectFlowsClient(_apiClient);
            Refunds = new RefundsClient(_apiClient);
            Subscriptions = new SubscriptionsClient(_apiClient);
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