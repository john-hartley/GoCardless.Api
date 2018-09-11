using System;

namespace GoCardless.Api.MandateImportEntries
{
    public class MandateImportEntry
    {
        public DateTimeOffset CreatedAt { get; set; }
        public MandateImportEntriesLinks Links { get; set; }
        public string RecordIdentifier { get; set; }
    }
}