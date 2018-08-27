using Newtonsoft.Json;

namespace GoCardlessApi.Mandates
{
    public class MandateLinks
    {
        public string Creditor { get; set; }

        public string Customer { get; set; }

        [JsonProperty("customer_bank_account")]
        public string CustomerBankAccount { get; set; }
    }
}