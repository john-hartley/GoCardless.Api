using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerNotifications
{
    public class CustomerNotificationsClient : ICustomerNotificationsClient
    {
        private readonly ApiClient _apiClient;

        public CustomerNotificationsClient(ApiClientConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<CustomerNotification>> HandleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"customer_notifications/{id}/actions/handle")
                    .PostJsonAsync(new { })
                    .ReceiveJson<Response<CustomerNotification>>();
            });
        }
    }
}