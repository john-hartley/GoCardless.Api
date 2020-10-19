using Newtonsoft.Json;

namespace GoCardlessApi.CustomerNotifications
{
    public class HandleCustomerNotificationOptions
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}