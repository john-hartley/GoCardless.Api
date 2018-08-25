using Newtonsoft.Json;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class CustomerBankAccountLinks
    {
        [JsonProperty("customer")]
        public string Customer { get; set; }
    }
}