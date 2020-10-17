using Newtonsoft.Json;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class DisableCustomerBankAccountOptions
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}