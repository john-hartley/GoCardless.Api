using Newtonsoft.Json;

namespace GoCardless.Api.MandateImports
{
    public class MandateImportResponse
    {
        [JsonProperty("mandate_imports")]
        public MandateImport MandateImport { get; set; }
    }
}