using Newtonsoft.Json;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class DisableCustomerBankAccountRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}