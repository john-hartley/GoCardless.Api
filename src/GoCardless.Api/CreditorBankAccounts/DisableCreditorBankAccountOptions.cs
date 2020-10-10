using Newtonsoft.Json;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class DisableCreditorBankAccountOptions
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}