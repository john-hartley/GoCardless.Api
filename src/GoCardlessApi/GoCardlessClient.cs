using GoCardlessApi.BankDetailsLookups;
using GoCardlessApi.Core;
using GoCardlessApi.CreditorBankAccounts;
using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.Customers;
using GoCardlessApi.MandatePdfs;
using GoCardlessApi.Mandates;
using GoCardlessApi.Payments;
using GoCardlessApi.Payouts;
using GoCardlessApi.RedirectFlows;
using GoCardlessApi.Refunds;
using GoCardlessApi.Subscriptions;

namespace GoCardlessApi
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
            MandatePdfs = new MandatePdfsClient(configuration);
            Payments = new PaymentsClient(configuration);
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
        public IMandatePdfsClient MandatePdfs { get; }
        public IMandatesClient Mandates { get; }
        public IPaymentsClient Payments { get; }
        public IPayoutsClient Payouts { get; }
        public IRedirectFlowsClient RedirectFlows { get; }
        public IRefundsClient Refunds { get; }
        public ISubscriptionsClient Subscriptions { get; }
    }
}