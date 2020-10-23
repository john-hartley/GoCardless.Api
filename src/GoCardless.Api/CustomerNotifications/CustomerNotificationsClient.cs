using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.CustomerNotifications
{
    public class CustomerNotificationsClient : ICustomerNotificationsClient
    {
        private readonly ApiClient _apiClient;

        public CustomerNotificationsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<CustomerNotification>> HandleAsync(HandleCustomerNotificationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment($"customer_notifications/{options.Id}/actions/handle")
                    .PostJsonAsync(new { })
                    .ReceiveJson<Response<CustomerNotification>>();
            });
        }
    }
}