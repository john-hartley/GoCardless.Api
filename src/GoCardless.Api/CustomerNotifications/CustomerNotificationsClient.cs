using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerNotifications
{
    public class CustomerNotificationsClient : ApiClientBase, ICustomerNotificationsClient
    {
        public CustomerNotificationsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<Response<CustomerNotification>> HandleAsync(string customerNotificationId)
        {
            if (string.IsNullOrWhiteSpace(customerNotificationId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(customerNotificationId));
            }

            return PostAsync<Response<CustomerNotification>>(
                $"customer_notifications/{customerNotificationId}/actions/handle"
            );
        }
    }
}