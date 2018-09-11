namespace GoCardless.Api.MandateImportEntries
{
    public class BankAccount
    {
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string CountryCode { get; set; }
        public string Iban { get; set; }
    }
}