using GoCardless.Api.Core.Http;
using System;

namespace GoCardless.Api.MandateImportEntries
{
    public class GetMandateImportEntriesRequest : IPageRequest, ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        public string Before { get; set; }
        public string After { get; set; }
        public int? Limit { get; set; }

        [QueryStringKey("mandate_import")]
        public string MandateImport { get; set; }
    }
}
