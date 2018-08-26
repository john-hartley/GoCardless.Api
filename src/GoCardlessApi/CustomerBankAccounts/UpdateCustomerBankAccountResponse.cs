using Newtonsoft.Json;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class UpdateCustomerBankAccountResponse
    {
        [JsonProperty("customer_bank_accounts")]
        public CustomerBankAccount CustomerBankAccount { get; set; }
    }
}