﻿namespace GoCardlessApi.MandateImportEntries
{
    public class AddMandateImportEntryOptions
    {
        public Amendment Amendment { get; set; }
        public BankAccount BankAccount { get; set; }
        public Customer Customer { get; set; }
        public AddMandateImportEntryLinks Links { get; set; }
        public string RecordIdentifier { get; set; }
    }
}