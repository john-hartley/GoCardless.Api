using Newtonsoft.Json;

namespace GoCardlessApi.Mandates
{
    public class CreateMandateLinks
    {
        [JsonProperty("customer_bank_account")]
        public string CustomerBankAccount { get; set; }

        [JsonProperty("creditor")]
        public string Creditor { get; set; }
    }
}