using GoCardless.Api.CreditorBankAccounts;
using Newtonsoft.Json;

namespace GoCardless.Api.Core
{
    public class Response<TResource> where TResource : class
    {
        public TResource Item { get; set; }

        [JsonProperty("creditor_bank_accounts")]
        private CreditorBankAccount CreditorBankAccount { set => Item = value as TResource; }
    }
}