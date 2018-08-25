using Newtonsoft.Json;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class DisableCustomerBankAccountResponse
    {
        [JsonProperty("customer_bank_accounts")]
        public CustomerBankAccount CustomerBankAccount { get; set; }
    }
}