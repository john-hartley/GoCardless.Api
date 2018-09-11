using Newtonsoft.Json;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class CustomerBankAccountResponse
    {
        [JsonProperty("customer_bank_accounts")]
        public CustomerBankAccount CustomerBankAccount { get; set; }
    }
}