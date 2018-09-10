using Newtonsoft.Json;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class DisableCreditorBankAccountResponse
    {
        [JsonProperty("creditor_bank_accounts")]
        public CreditorBankAccount CreditorBankAccount { get; set; }
    }
}