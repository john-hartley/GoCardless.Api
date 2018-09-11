using GoCardless.Api.Core;
using System.Collections.Generic;

namespace GoCardless.Api.MandateImportEntries
{
    public class AllMandateImportEntriesResponse
    {
        public IEnumerable<MandateImportEntry> MandateImportEntries { get; set; }
        public Meta Meta { get; set; }
    }
}