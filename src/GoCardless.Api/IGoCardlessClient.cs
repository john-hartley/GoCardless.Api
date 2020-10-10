using GoCardless.Api.BankDetailsLookups;
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
    public interface IGoCardlessClient
    {
        IBankDetailsLookupsClient BankDetailsLookups { get; }
        ICreditorBankAccountsClient CreditorBankAccounts { get; }
        ICreditorsClient Creditors { get; }
        ICustomerBankAccountsClient CustomerBankAccounts { get; }
        ICustomerNotificationsClient CustomerNotifications { get; }
        ICustomersClient Customers { get; }
        IEventsClient Events { get; }
        IMandateImportEntriesClient MandateImportEntries { get; }
        IMandateImportsClient MandateImports { get; }
        IMandatePdfsClient MandatePdfs { get; }
        IMandatesClient Mandates { get; }
        IPaymentsClient Payments { get; }
        IPayoutItemsClient PayoutItems { get; }
        IPayoutsClient Payouts { get; }
        IRedirectFlowsClient RedirectFlows { get; }
        IRefundsClient Refunds { get; }
        ISubscriptionsClient Subscriptions { get; }
    }
}