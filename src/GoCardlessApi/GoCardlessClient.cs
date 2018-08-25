using GoCardlessApi.Core;
using GoCardlessApi.CreditorBankAccounts;
using GoCardlessApi.Creditors;
using GoCardlessApi.Subscriptions;

namespace GoCardlessApi
{
    public class GoCardlessClient
    {
        private readonly ClientConfiguration _configuration;

        public GoCardlessClient(ClientConfiguration configuration)
        {
            _configuration = configuration;

            CreditorBankAccounts = new CreditorBankAccountsClient(configuration);
            Creditors = new CreditorsClient(configuration);
            Subscriptions = new SubscriptionsClient(configuration);
        }

        public ICreditorBankAccountsClient CreditorBankAccounts { get; }
        public ICreditorsClient Creditors { get; }
        public ISubscriptionsClient Subscriptions { get; }
    }
}