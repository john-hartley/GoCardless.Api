using Newtonsoft.Json;

namespace GoCardlessApi.Mandates
{
    public class MandateLinks
    {
        [JsonProperty("customer_bank_account")]
        public string CustomerBankAccount { get; set; }

        public string Creditor { get; set; }

        public string Customer { get; set; }
    }
}