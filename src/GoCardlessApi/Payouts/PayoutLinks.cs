using Newtonsoft.Json;

namespace GoCardlessApi.Payouts
{
    public class PayoutLinks
    {
        public string Creditor { get; set; }
        public string CreditorBankAccount { get; set; }
    }
}