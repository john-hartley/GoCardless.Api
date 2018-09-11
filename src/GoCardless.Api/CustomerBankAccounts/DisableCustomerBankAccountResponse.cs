using Newtonsoft.Json;

namespace GoCardless.Api.CustomerBankAccounts
{
    public class DisableCustomerBankAccountResponse
    {
        [JsonProperty("customer_bank_accounts")]
        public CustomerBankAccount CustomerBankAccount { get; set; }
    }
}