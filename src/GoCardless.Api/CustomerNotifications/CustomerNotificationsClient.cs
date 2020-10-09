using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerNotifications
{
    public class CustomerNotificationsClient : ApiClient, ICustomerNotificationsClient
    {
        public CustomerNotificationsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<Response<CustomerNotification>> HandleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return PostAsync<Response<CustomerNotification>>(
                $"customer_notifications/{id}/actions/handle"
            );
        }
    }
}