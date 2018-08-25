using Newtonsoft.Json;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class CreditorBankAccountLinks
    {
        [JsonProperty("creditor")]
        public string Creditor { get; set; }
    }
}