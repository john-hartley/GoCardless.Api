using Newtonsoft.Json;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class DisableCreditorBankAccountRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}