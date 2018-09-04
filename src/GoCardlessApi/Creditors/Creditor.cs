using System;

namespace GoCardlessApi.Creditors
{
    public class Creditor
    {
        public string Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public bool CanCreateRefunds { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public CreditorLinks Links { get; set; }
        public string LogoUrl { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string VerificationStatus { get; set; }
    }
}