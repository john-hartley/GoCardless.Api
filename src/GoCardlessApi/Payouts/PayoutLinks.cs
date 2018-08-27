using Newtonsoft.Json;

namespace GoCardlessApi.Payouts
{
    public class PayoutLinks
    {
        public string Creditor { get; set; }

        [JsonProperty("creditor_bank_account")]
        public string CreditorBankAccount { get; set; }
    }
}