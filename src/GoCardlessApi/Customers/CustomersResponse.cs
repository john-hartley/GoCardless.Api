using Newtonsoft.Json;

namespace GoCardlessApi.Customers
{
    public class CustomersResponse
    {
        [JsonProperty("customers")]
        public Customer Customer { get; set; }
    }
}