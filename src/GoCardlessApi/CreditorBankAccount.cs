using Newtonsoft.Json;

namespace GoCardlessApi
{
    public class CreditorBankAccount
    {
        public string Id { get; set; }

        [JsonProperty("account_holder_name")]
        public string AccountHolderName { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("branch_code")]
        public string BranchCode { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        public string Currency { get; set; }

        public CreditorBankAccountLinks Links { get; set; }
    }
}