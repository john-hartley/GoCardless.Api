using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoCardless.Api.Customers
{
    public class CreateCustomerRequest
    {
        public CreateCustomerRequest()
        {
            IdempotencyKey = Guid.NewGuid().ToString();
        }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string CountryCode { get; set; }
        public string DanishIdentityNumber { get; set; }
        public string Email { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }

        [JsonIgnore]
        public string IdempotencyKey { get; set; }

        public string Language { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string SwedishIdentityNumber { get; set; }
    }
}