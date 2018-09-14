using Newtonsoft.Json;

namespace GoCardless.Api.MandateImportEntries
{
    public class MandateImportEntryResponse
    {
        [JsonProperty("mandate_import_entries")]
        public MandateImportEntry MandateImportEntry { get; set; }
    }
}