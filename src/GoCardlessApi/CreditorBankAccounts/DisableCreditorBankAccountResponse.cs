using Newtonsoft.Json;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class DisableCreditorBankAccountResponse
    {
        [JsonProperty("creditor_bank_accounts")]
        public CreditorBankAccount CreditorBankAccount { get; set; }
    }
}