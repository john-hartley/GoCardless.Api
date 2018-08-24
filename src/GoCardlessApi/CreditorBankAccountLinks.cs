using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class CreditorBankAccountLinks
    {
        [JsonProperty("creditor")]
        public string Creditor { get; set; }
    }
}