using System.Collections.Generic;

namespace GoCardlessApi.BankDetailsLookups
{
    public class BankDetailsLookup
    {
        /// <summary>
        /// An array of schemes supported for this bank account. 
        /// See <see cref="Common.Scheme"/> for supported schemes.
        /// </summary>
        public IEnumerable<string> AvailableDebitSchemes { get; set; }

        public string BankName { get; set; }
        public string Bic { get; set; }
    }
}