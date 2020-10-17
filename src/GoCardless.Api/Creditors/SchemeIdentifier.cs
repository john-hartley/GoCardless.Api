namespace GoCardless.Api.Creditors
{
    public class SchemeIdentifier
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public bool CanSpecifyMandateReference { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public int MinimumAdvanceNotice { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Reference { get; set; }
        public string Region { get; set; }

        /// <summary>
        /// See <see cref="Common.Scheme"/> for supported schemes.
        /// </summary>
        public string Scheme { get; set; }
    }
}