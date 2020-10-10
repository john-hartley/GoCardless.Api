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
            _apiClient = apiClient;
        }

        public async Task<Response<CustomerNotification>> HandleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.PostAsync<Response<CustomerNotification>>(
                request =>
                {
                    request.AppendPathSegment($"customer_notifications/{id}/actions/handle");
                });
        }
    }
}