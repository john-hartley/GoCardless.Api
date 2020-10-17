using Newtonsoft.Json;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class DisableCreditorBankAccountOptions
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}