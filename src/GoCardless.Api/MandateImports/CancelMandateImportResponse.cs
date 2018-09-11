using Newtonsoft.Json;

namespace GoCardless.Api.MandateImports
{
    public class CancelMandateImportResponse
    {
        [JsonProperty("mandate_imports")]
        public MandateImport MandateImport { get; set; }
    }
}