using GoCardlessApi.Core;
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

            Creditors = new CreditorsClient(configuration);
            Subscriptions = new SubscriptionsClient(configuration);
        }

        public ICreditorsClient Creditors { get; }
        public ISubscriptionsClient Subscriptions { get; }
    }
}