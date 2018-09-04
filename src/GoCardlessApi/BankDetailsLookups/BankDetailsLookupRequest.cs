﻿namespace GoCardlessApi.BankDetailsLookups
{
    public class BankDetailsLookupRequest
    {
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string CountryCode { get; set; }
        public string Iban { get; set; }
    }
}