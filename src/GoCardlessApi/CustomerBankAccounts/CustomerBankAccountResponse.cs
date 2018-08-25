using Newtonsoft.Json;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class CustomerBankAccountResponse
    {
        [JsonProperty("customer_bank_accounts")]
        public CustomerBankAccount CustomerBankAccount { get; set; }
    }
}