namespace GoCardlessApi.MandateImportEntries
{
    public class CreateMandateImportEntryOptions
    {
        public Amendment Amendment { get; set; }
        public BankAccount BankAccount { get; set; }
        public Customer Customer { get; set; }
        public CreateMandateImportEntryLinks Links { get; set; }
        public string RecordIdentifier { get; set; }
    }
}