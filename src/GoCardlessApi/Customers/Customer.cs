using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardlessApi.Customers
{
    public class Customer
    {
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

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("danish_identity_number")]
        public string DanishIdentityNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("postal_code")]
        public string PostCode { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("swedish_identity_number")]
        public string SwedishIdentityNumber { get; set; }
    }
}