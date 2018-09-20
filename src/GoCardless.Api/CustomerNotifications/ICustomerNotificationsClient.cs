using System.Threading.Tasks;
using GoCardless.Api.Core.Http;

namespace GoCardless.Api.CustomerNotifications
{
    public interface ICustomerNotificationsClient
    {
        Task<Response<CustomerNotification>> HandleAsync(string customerNotificationId);
    }
}