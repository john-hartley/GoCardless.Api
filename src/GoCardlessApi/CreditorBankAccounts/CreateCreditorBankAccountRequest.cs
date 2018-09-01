using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.CreditorBankAccounts
{
    public class CreateCreditorBankAccountRequest
    {
        public CreateCreditorBankAccountRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        public string AccountHolderName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AccountNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BranchCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Iban { get; set; }

        public CreditorBankAccountLinks Links { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public bool SetAsDefaultPayoutAccount { get; set; }
    }
}