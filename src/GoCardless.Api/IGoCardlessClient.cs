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

namespace GoCardlessApi
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