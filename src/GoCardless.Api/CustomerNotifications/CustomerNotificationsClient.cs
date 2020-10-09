using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.CustomerNotifications
{
    public class CustomerNotificationsClient : ApiClient, ICustomerNotificationsClient
    {
        private readonly IApiClient _apiClient;

        public CustomerNotificationsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
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
                "customer_notifications",
                null,
                request =>
                {
                    request.AppendPathSegment($"customer_notifications/{id}/actions/handle");
                });
        }
    }
}