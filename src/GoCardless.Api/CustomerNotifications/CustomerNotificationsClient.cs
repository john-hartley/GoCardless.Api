using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerNotifications
{
    public class CustomerNotificationsClient : ICustomerNotificationsClient
    {
        private readonly IApiClient _apiClient;

        public CustomerNotificationsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public CustomerNotificationsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
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