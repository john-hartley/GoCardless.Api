using GoCardless.Api.BankDetailsLookups;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.CreditorBankAccounts;
using GoCardless.Api.Creditors;
using GoCardless.Api.CustomerBankAccounts;
using GoCardless.Api.Customers;
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

            BankDetailsLookups = new BankDetailsLookupsClient(configuration);
            CreditorBankAccounts = new CreditorBankAccountsClient(configuration);
            Creditors = new CreditorsClient(configuration);
            Customers = new CustomersClient(configuration);
            CustomerBankAccounts = new CustomerBankAccountsClient(configuration);
            Mandates = new MandatesClient(configuration);
            MandateImports = new MandateImportsClient(configuration);
            MandatePdfs = new MandatePdfsClient(configuration);
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
        public ICustomersClient Customers { get; }
        public IMandateImportsClient MandateImports { get; }
        public IMandatePdfsClient MandatePdfs { get; }
        public IMandatesClient Mandates { get; }
        public IPaymentsClient Payments { get; }
        public IPayoutItemsClient PayoutItems { get; set; }
        public IPayoutsClient Payouts { get; }
        public IRedirectFlowsClient RedirectFlows { get; }
        public IRefundsClient Refunds { get; }
        public ISubscriptionsClient Subscriptions { get; }
    }
}