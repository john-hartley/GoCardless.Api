using Newtonsoft.Json;

namespace GoCardlessApi.Customers
{
    public class CreateCustomerResponse
    {
        [JsonProperty("customers")]
        public Customer Customer { get; set; }
    }
}