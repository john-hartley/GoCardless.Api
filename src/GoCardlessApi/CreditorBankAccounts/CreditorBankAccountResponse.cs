using Newtonsoft.Json;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class CreditorBankAccountResponse
    {
        [JsonProperty("creditor_bank_accounts")]
        public CreditorBankAccount CreditorBankAccount { get; set; }
    }
}