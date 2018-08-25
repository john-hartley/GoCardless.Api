using Newtonsoft.Json;

namespace GoCardlessApi.Customers
{
    public class UpdateCustomerResponse
    {
        [JsonProperty("customers")]
        public Customer Customer { get; set; }
    }
}