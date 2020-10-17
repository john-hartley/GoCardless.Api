using System.Threading.Tasks;
using GoCardlessApi.Http;

namespace GoCardlessApi.CustomerNotifications
{
    public interface ICustomerNotificationsClient
    {
        Task<Response<CustomerNotification>> HandleAsync(string id);
    }
}