using Newtonsoft.Json;
using System;

namespace GoCardlessApi.Creditors
{
    public class Creditor
    {
        public string Id { get; set; }

        [JsonProperty("address_line1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("address_line2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("address_line3")]
        public string AddressLine3 { get; set; }

        [JsonProperty("can_create_refunds")]
        public bool CanCreateRefunds { get; set; }

        public string City { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        public string Name { get; set; }

        [JsonProperty("postal_code")]
        public string PostCode { get; set; }

        public string Region { get; set; }

        [JsonProperty("verification_status")]
        public string VerificationStatus { get; set; }
    }
}