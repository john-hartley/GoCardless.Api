using Newtonsoft.Json;

namespace GoCardless.Api.CreditorBankAccounts
{
    public class CreateCreditorBankAccountResponse
    {
        [JsonProperty("creditor_bank_accounts")]
        public CreditorBankAccount CreditorBankAccount { get; set; }
    }
}