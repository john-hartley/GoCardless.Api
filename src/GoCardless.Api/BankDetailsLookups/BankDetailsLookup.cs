using System.Collections.Generic;

namespace GoCardless.Api.BankDetailsLookups
{
    public class BankDetailsLookup
    {
        /// <summary>
        /// An array of schemes supported for this bank account. 
        /// See <see cref="Models.Scheme"/> for supported schemes.
        /// </summary>
        public IEnumerable<string> AvailableDebitSchemes { get; set; }

        public string BankName { get; set; }
        public string Bic { get; set; }
    }
}