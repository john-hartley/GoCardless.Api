using Newtonsoft.Json;

namespace GoCardlessApi.Customers
{
    public class CustomerResponse
    {
        [JsonProperty("customers")]
        public Customer Customer { get; set; }
    }
}