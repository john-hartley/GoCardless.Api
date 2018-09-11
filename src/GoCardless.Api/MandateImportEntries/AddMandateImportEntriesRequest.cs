namespace GoCardless.Api.MandateImportEntries
{
    public class AddMandateImportEntriesRequest
    {
        public Amendment Amendment { get; set; }
        public BankAccount BankAccount { get; set; }
        public Customer Customer { get; set; }
        public string RecordIdentifier { get; set; }
        public MandateImportEntriesLinks Links { get; set; }
    }
}