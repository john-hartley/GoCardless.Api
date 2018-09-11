using Newtonsoft.Json;

namespace GoCardless.Api.MandateImportEntries
{
    public class AddMandateImportEntryResponse
    {
        [JsonProperty("mandate_import_entries")]
        public MandateImportEntry MandateImportEntry { get; set; }
    }
}