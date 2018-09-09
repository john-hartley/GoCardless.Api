﻿using System.Collections.Generic;

namespace GoCardless.Api.BankDetailsLookups
{
    public class BankDetailsLookup
    {
        public IEnumerable<string> AvailableDebitSchemes { get; set; }
        public string BankName { get; set; }
        public string Bic { get; set; }
    }
}