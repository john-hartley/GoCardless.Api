using Newtonsoft.Json;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class DisableCustomerBankAccountRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}