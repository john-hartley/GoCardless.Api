using Newtonsoft.Json;

namespace GoCardless.Api.BankDetailsLookups
{
    public class BankDetailsLookupResponse
    {
        [JsonProperty("bank_details_lookups")]
        public BankDetailsLookup BankDetailsLookup { get; set; }
    }
}