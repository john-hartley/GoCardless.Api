using GoCardless.Api.Http;
using GoCardless.Api.Serialisation;
using System;

namespace GoCardless.Api.MandateImportEntries
{
    public class GetMandateImportEntriesOptions : IPageOptions, ICloneable
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
