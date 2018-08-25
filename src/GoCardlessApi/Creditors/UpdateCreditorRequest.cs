using Newtonsoft.Json;

namespace GoCardlessApi.Creditors
{
    public class UpdateCreditorRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
        [JsonProperty("address_line1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("address_line2")]
        public string AddressLine2 { get; set; }
        [JsonProperty("address_line3")]
        public string AddressLine3 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("postal_code")]
        public string PostCode { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        //[JsonProperty("logo_url")]
        //public string LogoUrl { get; set; }
    }
}