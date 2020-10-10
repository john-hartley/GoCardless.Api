using Newtonsoft.Json;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class DisableCustomerBankAccountOptions
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}