using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoCardlessApi.CustomerBankAccounts
{
    public class CreateCustomerBankAccountRequest
    {
        public CreateCustomerBankAccountRequest()
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

        public CustomerBankAccountLinks Links { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}