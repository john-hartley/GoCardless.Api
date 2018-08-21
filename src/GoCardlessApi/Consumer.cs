using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class Consumer
    {
        public async Task DoStuff()
        {
            var api = new GoCardlessClient(ClientConfiguration.ForSandbox(""));
            await api.Subscriptions.CreateAsync(null);

            var client = new SubscriptionsClient(ClientConfiguration.ForSandbox(""));
            var subscription = await client.ForIdAsync("");
        }
    }
}