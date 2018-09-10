using Newtonsoft.Json;

namespace GoCardless.Api.Customers
{
    public class CreateCustomerResponse
    {
        [JsonProperty("customers")]
        public Customer Customer { get; set; }
    }
}