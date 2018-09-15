using GoCardless.Api.CreditorBankAccounts;
using GoCardless.Api.Creditors;
using Newtonsoft.Json;

namespace GoCardless.Api.Core
{
    public class Response<TResource> where TResource : class
    {
        public TResource Item { get; set; }

        [JsonProperty("creditor_bank_accounts")]
        private CreditorBankAccount CreditorBankAccount { set => Item = value as TResource; }

        [JsonProperty("creditors")]
        private Creditor Creditor { set => Item = value as TResource; }
    }
}