using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi
{
    public class CreateCreditorBankAccountRequest
    {
        public CreateCreditorBankAccountRequest()
        {
            Metadata = new Dictionary<string, string>();
        }

        [JsonProperty("account_holder_name")]
        public string AccountHolderName { get; set; }

        [JsonProperty("account_number", NullValueHandling = NullValueHandling.Ignore)]
        public string AccountNumber { get; set; }

        [JsonProperty("bank_code", NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; }

        [JsonProperty("branch_code", NullValueHandling = NullValueHandling.Ignore)]
        public string BranchCode { get; set; }

        [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }

        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("iban", NullValueHandling = NullValueHandling.Ignore)]
        public string Iban { get; set; }

        [JsonProperty("links")]
        public CreditorBankAccountLinks Links { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }
    }
}