using Newtonsoft.Json;

namespace GoCardless.Api.Creditors
{
    public class UpdateCreditorOptions
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public CreditorLinks Links { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
    }
}