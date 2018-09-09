using Newtonsoft.Json;

namespace GoCardless.Api.Customers
{
    public class CustomerResponse
    {
        [JsonProperty("customers")]
        public Customer Customer { get; set; }
    }
}