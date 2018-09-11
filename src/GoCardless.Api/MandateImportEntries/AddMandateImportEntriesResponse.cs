using Newtonsoft.Json;

namespace GoCardless.Api.MandateImportEntries
{
    public class AddMandateImportEntriesResponse
    {
        [JsonProperty("mandate_import_entries")]
        public MandateImportEntry MandateImportEntry { get; set; }
    }
}