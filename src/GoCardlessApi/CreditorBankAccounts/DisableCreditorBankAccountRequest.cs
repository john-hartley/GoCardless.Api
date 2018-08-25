using Newtonsoft.Json;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class DisableCreditorBankAccountRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}