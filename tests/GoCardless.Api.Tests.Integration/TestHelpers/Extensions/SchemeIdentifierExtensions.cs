using GoCardlessApi.Creditors;

namespace GoCardlessApi.Tests.Integration.TestHelpers.Extensions
{
    internal static class SchemeIdentifierExtensions
    {
        internal static bool IsValid(this SchemeIdentifier source)
        {
            return source.AddressLine1 != null
                && source.CanSpecifyMandateReference
                && source.City != null
                && source.CountryCode != null
                && source.Currency != null
                && source.MinimumAdvanceNotice != default
                && source.Name != null
                && source.PhoneNumber != null
                && source.PostalCode != null
                && source.Region != null
                && source.Reference != null
                && source.Scheme != null;
        }
    }
}