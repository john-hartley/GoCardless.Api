using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class CreateCreditorBankAccountResponse
    {
        [JsonProperty("creditor_bank_accounts")]
        public CreditorBankAccount CreditorBankAccount { get; set; }
    }
}