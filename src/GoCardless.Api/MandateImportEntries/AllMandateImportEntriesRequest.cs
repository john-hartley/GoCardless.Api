using GoCardless.Api.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoCardless.Api.MandateImportEntries
{
    public class AllMandateImportEntriesRequest : IPageRequest
    {
        public string Before { get; set; }
        public string After { get; set; }
        public int? Limit { get; set; }

        [QueryStringKey("mandate_import")]
        public string MandateImport { get; set; }
    }
}
