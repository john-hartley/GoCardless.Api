using Newtonsoft.Json;

namespace GoCardlessApi.BankDetailsLookups
{
    public class BankDetailsLookupResponse
    {
        [JsonProperty("bank_details_lookups")]
        public BankDetailsLookup BankDetailsLookup { get; set; }
    }
}