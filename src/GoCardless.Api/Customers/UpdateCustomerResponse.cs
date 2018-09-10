using Newtonsoft.Json;

namespace GoCardless.Api.Customers
{
    public class UpdateCustomerResponse
    {
        [JsonProperty("customers")]
        public Customer Customer { get; set; }
    }
}